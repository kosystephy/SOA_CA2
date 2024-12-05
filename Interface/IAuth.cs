using SOA_CA2_E_Commerce.DTO;

namespace SOA_CA2_E_Commerce.Interface
{
    public interface IAuth
    {
        Task<bool> RegisterAsync(RegisterDTO registerDto);
        Task<string> LoginAsync(LoginDTO loginDto);
        Task<string> GenerateApiKeyAsync(int userId);
    }
}
