using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SOA_CA2_E_Commerce.Data;
using SOA_CA2_E_Commerce.Services;
using SOA_CA2_E_Commerce.Models;
using SOA_CA2_E_Commerce.DTO;
using System.Threading.Tasks;

public class AuthServiceTests
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        // Setup InMemory Database
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _context = new ApplicationDbContext(options);

        // Mock Configuration
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddInMemoryCollection(new[]
        {
            new KeyValuePair<string, string>("JwtSettings:Secret", "SuperSecretKey"),
            new KeyValuePair<string, string>("JwtSettings:ExpiryMinutes", "30")
        });
        _configuration = configurationBuilder.Build();

        _authService = new AuthService(_context, _configuration);
    }

    [Fact]
    public async Task Register_ShouldReturnApiKey_WhenUserIsRegistered()
    {
        // Arrange
        var registerDto = new RegisterDTO
        {
            First_Name = "Test",
            Last_Name = "User",
            Email = "test@example.com",
            Password = "password",
            Role = 0,
            Address = "123 Street"
        };

        // Act
        var apiKey = await _authService.Register(registerDto);

        // Assert
        Assert.NotNull(apiKey);
        Assert.NotEmpty(apiKey);

        // Verify that the user was added to the database
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == registerDto.Email);
        Assert.NotNull(user);
        Assert.Equal(registerDto.Email, user.Email);
    }

    [Fact]
    public async Task Register_ShouldThrowException_WhenEmailIsAlreadyRegistered()
    {
        // Arrange
        var registerDto = new RegisterDTO
        {
            First_Name = "Test",
            Last_Name = "User",
            Email = "test@example.com",
            Password = "password",
            Role = 0,
            Address = "123 Street"
        };

        await _authService.Register(registerDto);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await _authService.Register(registerDto); // Attempt to register with the same email
        });
    }
}
