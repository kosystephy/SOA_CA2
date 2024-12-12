namespace SOA_CA2_E_Commerce.Models
{
    public class CartItem
    {
        public int CartItem_Id { get; set; }
        public int Cart_Id { get; set; } 
        public Cart Cart { get; set; } 
        public int Product_Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
