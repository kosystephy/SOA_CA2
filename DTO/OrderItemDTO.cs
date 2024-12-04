namespace SOA_CA2_E_Commerce.DTO
{
    public class OrderItemDTO
    {
        public int OrderItem_Id { get; set; }
        public int Order_Id { get; set; }
        public int Product_Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

}
