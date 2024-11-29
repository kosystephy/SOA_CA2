namespace SOA_CA2_E_Commerce.DTO
{
    public class OrderItemsDTO
    {
        public int Item_Id { get; set; }
        public int Order_Id { get; set; }
        public int Product_Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Quantity * Price;  // Computed Property
    }

}
