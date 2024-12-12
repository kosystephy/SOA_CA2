namespace SOA_CA2_E_Commerce.Models
{
    public class User
    {
        public int User_Id { get; set; }  
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }  
        public string PasswordHash { get; set; }  
        public string Salt { get; set; }
        public Enums.UserRole? Role { get; set; } = Enums.UserRole.Customer; 
        public string Address { get; set; }

        public string ApiKey { get; set; }

        public string RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiration { get; set; }

        public DateTime? ApiKeyExpiration { get; set; } 
        public DateTime CreatedAt { get; set; }

       
        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
    }
}
 