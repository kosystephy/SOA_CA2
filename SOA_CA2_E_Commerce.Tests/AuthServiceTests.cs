using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;
using Moq;
using SOA_CA2_E_Commerce.Data;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Services;
using SOA_CA2_E_Commerce.Enums;
using SOA_CA2_E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using SOA_CA2_E_Commerce.Helpers;

public class AuthServiceTests
{
    private readonly AuthService _authService;
    private readonly ApplicationDbContext _dbContext;

    public AuthServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _dbContext = new ApplicationDbContext(options);

        var mockConfiguration = new Mock<IConfiguration>();
        mockConfiguration.Setup(config => config["JwtSettings:Secret"]).Returns("JcXfachH3n5UcEESM0VAPeAdQ4KzEw9d");
        mockConfiguration.Setup(config => config["JwtSettings:ExpiryMinutes"]).Returns("30");

        _authService = new AuthService(_dbContext, mockConfiguration.Object);
    }

    [Fact]
    public async Task Register_SuccessfullyReturnsApiKey()
    {
        var registerDto = new RegisterDTO
        {
            First_Name = "test",
            Last_Name = "testing",
            Email = "testing.testings@example.com",
            Password = "Password123!",
            Address = "123 Main Street",
            Role = UserRole.Customer
        };

        var apiKey = await _authService.Register(registerDto);

        Assert.NotNull(apiKey);
        Assert.IsType<string>(apiKey);
    }

    [Fact]
    public async Task Register_ThrowsExceptionForDuplicateEmail()
    {
     
        _dbContext.Users.Add(new User
        {
            First_Name = "Test",
            Last_Name = "User",
            Email = "testing.testings@example.com",
            Address = "123 Test Street",
            ApiKey = "APIKEY123",
            PasswordHash = PasswordHelper.HashPassword("Password123!", "salt123"), 
            Salt = "salt123"
        });
        await _dbContext.SaveChangesAsync();


        var registerDto = new RegisterDTO
        {
            Email = "testing.testings@example.com",
            First_Name = "Duplicate",
            Last_Name = "User",
            Password = "Password123!",
            Address = "456 Another Street",
            Role = UserRole.Customer
        };

        await Assert.ThrowsAsync<InvalidOperationException>(() => _authService.Register(registerDto));
    }


    [Fact]
    public async Task Login_ValidCredentials_ReturnsJwtToken()
    {

        var salt = PasswordHelper.GenerateSalt();
        var passwordHash = PasswordHelper.HashPassword("Password123!", salt);

       
        _dbContext.Users.Add(new User
        {
            User_Id = 1,
            First_Name = "test",
            Last_Name = "testing",
            Email = "testing.testings@example.com",
            Address = "123 Main St",
            ApiKey = "APIKEY123",
            PasswordHash = passwordHash,
            Salt = salt,
            Role = UserRole.Customer,
            CreatedAt = DateTime.UtcNow
        });
        await _dbContext.SaveChangesAsync();

        var loginDto = new LoginDTO { Email = "testing.testings@example.com", Password = "Password123!" };
        var result = await _authService.Login(loginDto);

  
        Assert.NotNull(result.JwtToken);
        Assert.NotNull(result.RefreshToken);
        Assert.Equal("APIKEY123", result.ApiKey);
        Assert.Equal(1, result.UserId);
        Assert.Equal(UserRole.Customer, result.Role);
    }

    [Fact]
    public async Task Login_InvalidPassword_ThrowsUnauthorizedAccessException()
    {
        var passwordHash = PasswordHelper.HashPassword("ValidPassword", "randomSalt");
        _dbContext.Users.Add(new User
        {
            User_Id = 1,
            First_Name = "test",
            Last_Name = "testing",
            Email = "test.testing@example.com",
            Address = "123 Main St",
            ApiKey = "APIKEY123",
            PasswordHash = "hashedPassword",
            Salt = "salt123"
        });
        await _dbContext.SaveChangesAsync();

        var loginDto = new LoginDTO { Email = "user@example.com", Password = "WrongPassword" };
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _authService.Login(loginDto));
    }
}

