namespace SOA_CA2_E_Commerce.Models
{
    public class CartItem
    {
        public int CartItem_Id { get; set; } // Primary key
        public int Cart_Id { get; set; } // Foreign key to Cart
        public Cart Cart { get; set; } // Navigation property
        public int Product_Id { get; set; } // Foreign key to Product
        public Product Product { get; set; } // Navigation property
        public int Quantity { get; set; } // Quantity of the product in the cart
    }
}
