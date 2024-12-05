using SOA_CA2_E_Commerce.DTO;


namespace SOA_CA2_E_Commerce.Interface
{
    public interface ICart
    {
        Task<CartDTO> GetCartByUserIdAsync(int userId);
        Task<bool> AddToCartAsync(int userId, CartItemDTO cartItemDto);
        Task<bool> RemoveFromCartAsync(int userId, int productId);
        Task<bool> ClearCartAsync(int userId);
    }
}
