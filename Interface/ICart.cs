using SOA_CA2_E_Commerce.DTO;


namespace SOA_CA2_E_Commerce.Interface
{
    public interface ICart
    {
        Task<IEnumerable<CartDTO>> GetAllCarts();
        Task<CartDTO> GetCartById(int id);
        Task<CartDTO> CreateCart(CartDTO cartDTO);
        Task<CartDTO> UpdateCart(int id, CartDTO cartDTO);
        Task DeleteCart(int id);
        Task<CartDTO> GetOrCreateCart(int userId);
    }
}
