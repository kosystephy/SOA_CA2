using SOA_CA2_E_Commerce.DTO;

namespace SOA_CA2_E_Commerce.Interface
{
    public interface ICartItem
    {
        Task<IEnumerable<CartItemDTO>> GetAllCartItems();
        Task<CartItemDTO> GetCartItemById(int id);
        Task<CartItemDTO> AddToCart(CartItemDTO cartItemDTO);
        Task<CartItemDTO> UpdateCartItem(int id, CartItemDTO cartItemDTO);
        Task DeleteCartItem(int id);
        Task<IEnumerable<CartItemDTO>> GetCartItemsByCartId(int cartId);
    }
}
