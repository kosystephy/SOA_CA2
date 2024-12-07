using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using SOA_CA2_E_Commerce.Controllers;
using SOA_CA2_E_Commerce.Interface;
using SOA_CA2_E_Commerce.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserControllerTests
{
    private readonly Mock<IUser> _mockUserService;
    private readonly UserController _userController;

    public UserControllerTests()
    {
        _mockUserService = new Mock<IUser>();
        _userController = new UserController(_mockUserService.Object);
    }

    [Fact]
    public async Task GetAllUsers_ShouldReturnOkResult_WithListOfUsers()
    {
        // Arrange
        var mockUsers = new List<UserDTO>
        {
            new UserDTO { User_Id = 1, First_Name = "John", Last_Name = "Doe", Email = "john@example.com" },
            new UserDTO { User_Id = 2, First_Name = "Jane", Last_Name = "Smith", Email = "jane@example.com" }
        };
        _mockUserService.Setup(service => service.GetAllUsers()).ReturnsAsync(mockUsers);

        // Act
        var result = await _userController.GetAllUsers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedUsers = Assert.IsAssignableFrom<List<UserDTO>>(okResult.Value);
        Assert.Equal(2, returnedUsers.Count);
    }

    [Fact]
    public async Task GetUserById_ShouldReturnNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        int userId = 99;
        _mockUserService.Setup(service => service.GetUserById(userId)).Throws<KeyNotFoundException>();

        // Act
        var result = await _userController.GetUserById(userId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task UpdateUser_ShouldReturnOkResult_WhenUserIsUpdated()
    {
        // Arrange
        int userId = 1;
        var updatedUser = new UserDTO
        {
            User_Id = 1,
            First_Name = "John",
            Last_Name = "Updated",
            Email = "john.updated@example.com"
        };

        _mockUserService.Setup(service => service.UpdateUser(userId, updatedUser)).ReturnsAsync(updatedUser);

        // Act
        var result = await _userController.UpdateUser(userId, updatedUser);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedUser = Assert.IsType<UserDTO>(okResult.Value);
        Assert.Equal("John", returnedUser.First_Name);
        Assert.Equal("Updated", returnedUser.Last_Name);
    }
}
