using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using MockQueryable.Moq;
using SOA_CA2_E_Commerce.Data;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Models;
using SOA_CA2_E_Commerce.Services;
using Xunit;

public class ProductServiceTests
{
    private readonly ProductService _productService;
    private readonly ApplicationDbContext _dbContext;

    public ProductServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _dbContext = new ApplicationDbContext(options);
        _productService = new ProductService(_dbContext);
    }

    [Fact]
    public async Task GetAllProductsAsync_ReturnsListOfProducts()
    {
        _dbContext.Products.AddRange(new Product
        {
            Product_Id = 1,
            Product_Name = "Product 1",
            Price = 10.0m,
            Stock = 100,
            Description = "Description 1",
            Category_Id = 1
        },
         new Product
         {
             Product_Id = 2,
             Product_Name = "Product 2",
             Price = 40.0m,
             Stock = 200,
             Description = "Description 2",
             Category_Id = 2
         });
        await _dbContext.SaveChangesAsync();

        var result = await _productService.GetAllProductsAsync();

        Assert.Equal(2, result.Count);
        Assert.Contains(result, p => p.Product_Name == "Product 1");
    }

    [Fact]
    public async Task AddProductAsync_ValidData_AddsProduct()
    {
        var productDto = new ProductDTO
        {
            Product_Id = 1,
            Product_Name = "Product 1",
            Price = 10.0m,
            Stock = 100,
            Description = "Description 1",
            Category_Id = 1
        };

        var result = await _productService.AddProductAsync(productDto);

        Assert.True(result);
        Assert.Single(_dbContext.Products);
    }
}