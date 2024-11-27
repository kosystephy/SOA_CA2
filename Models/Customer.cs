namespace SOA_CA2_E_Commerce.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }  // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }  // Unique field for login
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }  // For secure authentication
        public string Salt { get; set; }
        public string Role { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }  // Timestamp
        public DateTime UpdatedAt { get; set; }  // Timestamp

        // Navigation Property to Orders
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
