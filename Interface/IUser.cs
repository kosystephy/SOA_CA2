using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Enums;

namespace SOA_CA2_E_Commerce.Interface
{
    public interface IUser
    {
        // Basic CRUD Methods
     
         Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUserById(int id);
        Task<UserDTO> UpdateUser(int id, UserDTO userDTO);
        Task DeleteUser(int id);
        Task UpdateUserRole(int userId, UserRole newRole);
        Task<AdminUserDTO> GetAdminCustomerById(int id); // For Admin Operations
    }

}

