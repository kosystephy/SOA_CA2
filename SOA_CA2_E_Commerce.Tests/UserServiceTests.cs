using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using MockQueryable.Moq;
using SOA_CA2_E_Commerce.Data;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Enums;
using SOA_CA2_E_Commerce.Models;
using SOA_CA2_E_Commerce.Services;
using Xunit;

namespace SOA_CA2_E_Commerce.Tests
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly ApplicationDbContext _dbContext;

        public UserServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _userService = new UserService(_dbContext);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsListOfUsers()
        {
            _dbContext.Users.AddRange(new User
            {
                User_Id = 1,
                First_Name = "John",
                Last_Name = "Doe",
                Email = "john.doe@example.com",
                Address = "123 Main St",
                ApiKey = "APIKEY123",
                PasswordHash = "hashedPassword",
                Salt = "salt123"

            },
            new User
            {
                User_Id = 2,
                First_Name = "Jqne",
                Last_Name = "Smith",
                Email = "jane.smith@example.com",
                Address = "1234 Main St",
                ApiKey = "APIKEY345",
                PasswordHash = "hashedPassword1",
                Salt = "salt345"

            });
            await _dbContext.SaveChangesAsync();

            var result = await _userService.GetAllUsers();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, u => u.First_Name == "John");
        }

        [Fact]
        public async Task GetUserById_ValidId_ReturnsUser()
        {
            _dbContext.Users.Add(new User
            {
                User_Id = 1,
                First_Name = "John",
                Last_Name = "Doe",
                Email = "john.doe@example.com",
                Address = "123 Main St",
                ApiKey = "APIKEY123",
                PasswordHash = "hashedPassword",
                Salt = "salt123"

            });
            await _dbContext.SaveChangesAsync();

            var result = await _userService.GetUserById(1);

            Assert.NotNull(result);
            Assert.Equal("John", result.First_Name);
        }
    }
}