namespace SOA_CA2_E_Commerce.Models
{
    public class Cart
    {
        public int Cart_Id { get; set; } 
        public int User_Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public User User { get; set; } 
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>(); 
    }
}
