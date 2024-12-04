namespace SOA_CA2_E_Commerce.DTO
{
    public class CartItemDTO
    {
        public int CartItem_Id { get; set; }
        public int Cart_Id { get; set; } // FK to Cart
        public int Product_Id { get; set; } // FK to Product
        public int Quantity { get; set; }


    }
}
