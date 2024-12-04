using System.Security.Authentication;
using SOA_CA2_E_Commerce.Data;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Helpers;
using SOA_CA2_E_Commerce.Interface;
using Microsoft.EntityFrameworkCore;
using SOA_CA2_E_Commerce.Enums;

namespace SOA_CA2_E_Commerce.Services
{
    public class UserService : IUser
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            return await _context.Users.Select(u => new UserDTO
            {
                User_Id = u.User_Id,
                First_Name = u.First_Name,
                Last_Name = u.Last_Name,
                Email = u.Email,
                Address = u.Address,
            }).ToListAsync();
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) throw new KeyNotFoundException("User not found");

            return new UserDTO
            {
                User_Id = user.User_Id,
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                Email = user.Email,
                Address = user.Address,

            };
        }

        public async Task<UserDTO> UpdateUser(int id, UserDTO userDTO)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) throw new KeyNotFoundException("User not found");

            user.First_Name = userDTO.First_Name;
            user.Last_Name = userDTO.Last_Name;
            user.Email = userDTO.Email;
            user.Address = userDTO.Address;
        
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return userDTO;
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) throw new KeyNotFoundException("User not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserRole(int userId, UserRole newRole)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) throw new KeyNotFoundException("User not found");

            user.Role = newRole;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            // Add logging
            Console.WriteLine($"User {userId} role updated to {newRole}");
        }



        public async Task<AdminUserDTO> GetAdminCustomerById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) throw new KeyNotFoundException("User not found");

            return new AdminUserDTO
            {
                User_Id = user.User_Id,
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                Email = user.Email,
                Role = user.Role ?? UserRole.Customer, // Use default if Role is null
                Address = user.Address
            };
        }

    }
}
