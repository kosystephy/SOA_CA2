namespace SOA_CA2_E_Commerce.Models
{
    public class User
    {
        public int User_Id { get; set; }  // Primary Key
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }  // Unique field for login
        public string PasswordHash { get; set; }  // For secure authentication
        public string Salt { get; set; }
        public Enums.UserRole? Role { get; set; } = Enums.UserRole.Customer; // Enum for Role
        public string Address { get; set; }

        public string ApiKey { get; set; }

        public string RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiration { get; set; }

        public DateTime? ApiKeyExpiration { get; set; } // Expiration date for the API key
        public DateTime CreatedAt { get; set; }

        // Navigation Property to Orders
        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
    }
}
 