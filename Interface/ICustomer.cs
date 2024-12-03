using SOA_CA2_E_Commerce.DTO;

namespace SOA_CA2_E_Commerce.Interface
{
    public interface ICustomer
    {
        // Basic CRUD Methods
        Task<IEnumerable<CustomersDTO>> GetAllCustomers();
        Task<CustomersDTO> GetCustomerById(int id);
        Task<CustomersDTO> CreateCustomer(CustomersDTO customerDto);
        Task<CustomersDTO> UpdateCustomer(int id, CustomersDTO customerDto);
        Task DeleteCustomer(int id);

        // Custom Methods
        Task<CustomersDTO> GetCustomerByEmail(string email); // Retrieve by email
        Task<IEnumerable<CustomersDTO>> SearchCustomersByName(string name); // Search by name
    }
}
