namespace SOA_CA2_E_Commerce.Models
{
    public class Cart
    {
        public int Cart_Id { get; set; } // Primary key
        public int User_Id { get; set; } // Foreign key to User

        public DateTime CreatedAt { get; set; }
        public User User { get; set; } // Navigation property
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>(); // Cart items
    }
}
