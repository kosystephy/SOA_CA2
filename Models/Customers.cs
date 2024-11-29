namespace SOA_CA2_E_Commerce.Models
{
    public class Customers
    {
        public int Customer_Id { get; set; }  // Primary Key
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }  // Unique field for login
        public string PasswordHash { get; set; }  // For secure authentication
        public string Salt { get; set; }
        public Enums.UserRole Role { get; set; } = Enums.UserRole.Customer; // Enum for Role
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }  // Timestamp
        public DateTime UpdatedAt { get; set; }  // Timestamp

        // Navigation Property to Orders
        public ICollection<Orders> Orders { get; set; } = new List<Orders>();

        public ICollection<Auths> Auths { get; set; } = new List<Auths>();
    }
}
 