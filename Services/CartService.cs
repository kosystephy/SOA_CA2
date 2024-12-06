using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SOA_CA2_E_Commerce.Data;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Interface;
using SOA_CA2_E_Commerce.Models;

namespace SOA_CA2_E_Commerce.Services
{
    public class CartService : ICart
    {
        private readonly ApplicationDbContext _dbContext;

        public CartService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CartDTO> GetCartByUserIdAsync(int userId)
        {
            var cart = await _dbContext.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.User_Id == userId);

            if (cart == null) return null;

            return new CartDTO
            {
                Cart_Id = cart.Cart_Id,
                User_Id = cart.User_Id,
                CreatedAt = cart.CreatedAt,
                Items = cart.CartItems.Select(ci => new CartItemDTO
                {
                    CartItem_Id = ci.CartItem_Id,
                    Cart_Id = ci.Cart_Id,
                    Product_Id = ci.Product_Id,
                    Quantity = ci.Quantity
                }).ToList()
            };
        }

        public async Task<bool> AddToCartAsync(int userId, CartItemDTO cartItemDto)
        {
            var cart = await _dbContext.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.User_Id == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    User_Id = userId,
                    CreatedAt = DateTime.UtcNow
                };
                _dbContext.Carts.Add(cart);
                await _dbContext.SaveChangesAsync();
            }

            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.Product_Id == cartItemDto.Product_Id);
            if (existingItem != null)
            {
                existingItem.Quantity += cartItemDto.Quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    Product_Id = cartItemDto.Product_Id,
                    Quantity = cartItemDto.Quantity
                });
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFromCartAsync(int userId, int productId)
        {
            var cart = await _dbContext.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.User_Id == userId);

            if (cart == null) return false;

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Product_Id == productId);
            if (cartItem != null)
            {
                cart.CartItems.Remove(cartItem);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> ClearCartAsync(int userId)
        {
            var cart = await _dbContext.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.User_Id == userId);

            if (cart == null) return false;

            cart.CartItems.Clear();
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
