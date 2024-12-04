using SOA_CA2_E_Commerce.Data;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Interface;
using Microsoft.EntityFrameworkCore;
using SOA_CA2_E_Commerce.Models;

namespace SOA_CA2_E_Commerce.Services
{
    public class CartItemService : ICartItem
    {
        private readonly ApplicationDbContext _context;

        public CartItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItemDTO>> GetAllCartItems()
        {
            return await _context.CartItems
                .Select(ci => new CartItemDTO
                {
                    CartItem_Id = ci.CartItem_Id,
                    Cart_Id = ci.Cart_Id,
                    Product_Id = ci.Product_Id,
                    Quantity = ci.Quantity
                }).ToListAsync();
        }

        public async Task<CartItemDTO> GetCartItemById(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null) throw new KeyNotFoundException("CartItem not found");

            return new CartItemDTO
            {
                CartItem_Id = cartItem.CartItem_Id,
                Cart_Id = cartItem.Cart_Id,
                Product_Id = cartItem.Product_Id,
                Quantity = cartItem.Quantity
            };
        }

        public async Task<CartItemDTO> AddToCart(CartItemDTO cartItemDTO)
        {
            var cartItem = new CartItem
            {
                Cart_Id = cartItemDTO.Cart_Id,
                Product_Id = cartItemDTO.Product_Id,
                Quantity = cartItemDTO.Quantity
            };

            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();

            cartItemDTO.CartItem_Id = cartItem.CartItem_Id;
            return cartItemDTO;
        }

        public async Task<CartItemDTO> UpdateCartItem(int id, CartItemDTO cartItemDTO)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null) throw new KeyNotFoundException("CartItem not found");

            cartItem.Product_Id = cartItemDTO.Product_Id;
            cartItem.Quantity = cartItemDTO.Quantity;

            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();

            return cartItemDTO;
        }

        public async Task DeleteCartItem(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null) throw new KeyNotFoundException("CartItem not found");

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CartItemDTO>> GetCartItemsByCartId(int cartId)
        {
            return await _context.CartItems
                .Where(ci => ci.Cart_Id == cartId)
                .Select(ci => new CartItemDTO
                {
                    CartItem_Id = ci.CartItem_Id,
                    Cart_Id = ci.Cart_Id,
                    Product_Id = ci.Product_Id,
                    Quantity = ci.Quantity
                }).ToListAsync();
        }
    }
}
