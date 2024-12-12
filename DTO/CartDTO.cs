namespace SOA_CA2_E_Commerce.DTO
{
    public class CartDTO
    {
        public int Cart_Id { get; set; }
        public int User_Id { get; set; } 
        public DateTime CreatedAt { get; set; }
        public ICollection<CartItemDTO> Items { get; set; }
    }
}
