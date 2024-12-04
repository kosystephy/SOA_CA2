using SOA_CA2_E_Commerce.Data;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Interface;
using Microsoft.EntityFrameworkCore;
using SOA_CA2_E_Commerce.Models;

namespace SOA_CA2_E_Commerce.Services
{
    public class CartService : ICart
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartDTO>> GetAllCarts()
        {
            return await _context.Carts
                .Select(c => new CartDTO
                {
                    Cart_Id = c.Cart_Id,
                    User_Id = c.User_Id,
                    CreatedAt = c.CreatedAt
                }).ToListAsync();
        }

        public async Task<CartDTO> GetCartById(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null) throw new KeyNotFoundException("Cart not found");

            return new CartDTO
            {
                Cart_Id = cart.Cart_Id,
                User_Id = cart.User_Id,
                CreatedAt = cart.CreatedAt
            };
        }

        public async Task<CartDTO> CreateCart(CartDTO cartDTO)
        {
            var cart = new Cart
            {
                User_Id = cartDTO.User_Id,
                CreatedAt = DateTime.UtcNow
            };

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            cartDTO.Cart_Id = cart.Cart_Id;
            return cartDTO;
        }

        public async Task<CartDTO> UpdateCart(int id, CartDTO cartDTO)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null) throw new KeyNotFoundException("Cart not found");

            cart.User_Id = cartDTO.User_Id;

            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();

            return cartDTO;
        }

        public async Task DeleteCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null) throw new KeyNotFoundException("Cart not found");

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
        }

        public async Task<CartDTO> GetOrCreateCart(int userId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.User_Id == userId);
            if (cart == null)
            {
                cart = new Cart
                {
                    User_Id = userId,
                    CreatedAt = DateTime.UtcNow
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            return new CartDTO
            {
                Cart_Id = cart.Cart_Id,
                User_Id = cart.User_Id,
                CreatedAt = cart.CreatedAt
            };
        }
    }
}
