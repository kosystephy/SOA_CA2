using SOA_CA2_E_Commerce.Data;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using SOA_CA2_E_Commerce.Interface;

namespace SOA_CA2_E_Commerce.Services
{
    public class CustomerService : ICustomer
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all customers
        public async Task<IEnumerable<CustomersDTO>> GetAllCustomers()
        {
            return await _context.Customers
                .Select(c => new CustomersDTO
                {
                    Customer_Id = c.Customer_Id,
                    First_Name = c.First_Name,
                    Last_Name = c.Last_Name,
                    Email = c.Email,
                    Role = c.Role,
                    Address = c.Address,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                })
                .ToListAsync();
        }

        // Get a customer by ID
        public async Task<CustomersDTO> GetCustomerById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                throw new KeyNotFoundException("Customer not found");

            return new CustomersDTO
            {
                Customer_Id = customer.Customer_Id,
                First_Name = customer.First_Name,
                Last_Name = customer.Last_Name,
                Email = customer.Email,
                Role = customer.Role,
                Address = customer.Address,
                CreatedAt = customer.CreatedAt,
                UpdatedAt = customer.UpdatedAt
            };
        }

        // Create a new customer
        public async Task<CustomersDTO> CreateCustomer(CustomersDTO customerDto)
        {
            var customer = new Customers
            {
                First_Name = customerDto.First_Name,
                Last_Name = customerDto.Last_Name,
                Email = customerDto.Email,
                PasswordHash = "defaultHash", // Default value for simplicity
                Salt = "defaultSalt",         // Default value for simplicity
                Role = customerDto.Role,
                Address = customerDto.Address,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            customerDto.Customer_Id = customer.Customer_Id;
            return customerDto;
        }

        // Update an existing customer
        public async Task<CustomersDTO> UpdateCustomer(int id, CustomersDTO customerDto)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                throw new KeyNotFoundException("Customer not found");

            customer.First_Name = customerDto.First_Name;
            customer.Last_Name = customerDto.Last_Name;
            customer.Email = customerDto.Email;
            customer.Role = customerDto.Role;
            customer.Address = customerDto.Address;
            customer.UpdatedAt = DateTime.UtcNow;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return customerDto;
        }

        // Delete a customer
        public async Task DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                throw new KeyNotFoundException("Customer not found");

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        // Get a customer by email
        public async Task<CustomersDTO> GetCustomerByEmail(string email)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == email);

            if (customer == null)
                throw new KeyNotFoundException("Customer not found");

            return new CustomersDTO
            {
                Customer_Id = customer.Customer_Id,
                First_Name = customer.First_Name,
                Last_Name = customer.Last_Name,
                Email = customer.Email,
                Role = customer.Role,
                Address = customer.Address,
                CreatedAt = customer.CreatedAt,
                UpdatedAt = customer.UpdatedAt
            };
        }

        // Search customers by name
        public async Task<IEnumerable<CustomersDTO>> SearchCustomersByName(string name)
        {
            return await _context.Customers
                .Where(c => c.First_Name.Contains(name) || c.Last_Name.Contains(name))
                .Select(c => new CustomersDTO
                {
                    Customer_Id = c.Customer_Id,
                    First_Name = c.First_Name,
                    Last_Name = c.Last_Name,
                    Email = c.Email,
                    Role = c.Role,
                    Address = c.Address,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                })
                .ToListAsync();
        }
    }
}

