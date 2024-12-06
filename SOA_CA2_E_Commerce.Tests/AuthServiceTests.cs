using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SOA_CA2_E_Commerce.Data;
using SOA_CA2_E_Commerce.Services;
using SOA_CA2_E_Commerce.Models;
using SOA_CA2_E_Commerce.DTO;
using System.Threading.Tasks;

public class AuthServiceTests
{
    private readonly Mock<ApplicationDbContext> _mockContext;
    private readonly Mock<IConfiguration> _mockConfiguration;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _mockContext = new Mock<ApplicationDbContext>();
        _mockConfiguration = new Mock<IConfiguration>();
        _authService = new AuthService(_mockContext.Object, _mockConfiguration.Object);
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

        var mockUserDbSet = new Mock<DbSet<User>>();
        _mockContext.Setup(context => context.Users).Returns(mockUserDbSet.Object);
        _mockContext.Setup(context => context.SaveChangesAsync(default)).ReturnsAsync(1);

        // Act
        var apiKey = await _authService.Register(registerDto);

        // Assert
        Assert.NotNull(apiKey);
    }
}
