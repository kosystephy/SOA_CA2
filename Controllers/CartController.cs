using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using SOA_CA2_E_Commerce.Data;
using SOA_CA2_E_Commerce.Services;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CartServiceTests
{
    private readonly Mock<ApplicationDbContext> _mockDbContext;
    private readonly Mock<DbSet<Cart>> _mockCartDbSet;
    private readonly CartService _cartService;

    public CartServiceTests()
    {
        _mockDbContext = new Mock<ApplicationDbContext>();
        _mockCartDbSet = new Mock<DbSet<Cart>>();
        _mockDbContext.Setup(db => db.Carts).Returns(_mockCartDbSet.Object);
        _cartService = new CartService(_mockDbContext.Object);
    }

    private void SetupDbSetMock<T>(Mock<DbSet<T>> dbSetMock, List<T> data) where T : class
    {
        var queryable = data.AsQueryable();
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
    }

    [Fact]
    public async Task GetCartByUserIdAsync_ShouldReturnCart_WhenCartExists()
    {
        // Arrange
        var userId = 1;
        var cart = new Cart
        {
            Cart_Id = 1,
            User_Id = userId,
            CreatedAt = System.DateTime.UtcNow,
            CartItems = new List<CartItem>
            {
                new CartItem { CartItem_Id = 1, Product_Id = 1, Quantity = 2 },
                new CartItem { CartItem_Id = 2, Product_Id = 2, Quantity = 3 }
            }
        };
        var carts = new List<Cart> { cart };

        SetupDbSetMock(_mockCartDbSet, carts);

        // Act
        var result = await _cartService.GetCartByUserIdAsync(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(cart.Cart_Id, result.Cart_Id);
        Assert.Equal(cart.User_Id, result.User_Id);
        Assert.Equal(cart.CartItems.Count, result.Items.Count);
    }

    [Fact]
    public async Task AddToCartAsync_ShouldAddNewItem_WhenProductDoesNotExist()
    {
        // Arrange
        var userId = 1;
        var cartItemDto = new CartItemDTO { Product_Id = 1, Quantity = 2 };

        var cart = new Cart
        {
            User_Id = userId,
            CartItems = new List<CartItem>()
        };
        var carts = new List<Cart> { cart };

        SetupDbSetMock(_mockCartDbSet, carts);

        _mockDbContext.Setup(db => db.SaveChangesAsync(default)).ReturnsAsync(1);

        // Act
        var result = await _cartService.AddToCartAsync(userId, cartItemDto);

        // Assert
        Assert.True(result);
        Assert.Single(cart.CartItems);
        Assert.Equal(cartItemDto.Product_Id, cart.CartItems[0].Product_Id);
        Assert.Equal(cartItemDto.Quantity, cart.CartItems[0].Quantity);
    }

    [Fact]
    public async Task RemoveFromCartAsync_ShouldRemoveItem_WhenProductExists()
    {
        // Arrange
        var userId = 1;
        var productId = 1;

        var cart = new Cart
        {
            User_Id = userId,
            CartItems = new List<CartItem>
            {
                new CartItem { CartItem_Id = 1, Product_Id = productId, Quantity = 2 }
            }
        };
        var carts = new List<Cart> { cart };

        SetupDbSetMock(_mockCartDbSet, carts);

        _mockDbContext.Setup(db => db.SaveChangesAsync(default)).ReturnsAsync(1);

        // Act
        var result = await _cartService.RemoveFromCartAsync(userId, productId);

        // Assert
        Assert.True(result);
        Assert.Empty(cart.CartItems);
    }

    [Fact]
    public async Task ClearCartAsync_ShouldRemoveAllItems_WhenCartExists()
    {
        // Arrange
        var userId = 1;

        var cart = new Cart
        {
            User_Id = userId,
            CartItems = new List<CartItem>
            {
                new CartItem { CartItem_Id = 1, Product_Id = 1, Quantity = 2 },
                new CartItem { CartItem_Id = 2, Product_Id = 2, Quantity = 3 }
            }
        };
        var carts = new List<Cart> { cart };

        SetupDbSetMock(_mockCartDbSet, carts);

        _mockDbContext.Setup(db => db.SaveChangesAsync(default)).ReturnsAsync(1);

        // Act
        var result = await _cartService.ClearCartAsync(userId);

        // Assert
        Assert.True(result);
        Assert.Empty(cart.CartItems);
    }
}
