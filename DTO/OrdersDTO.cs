namespace SOA_CA2_E_Commerce.DTO
{
    public class OrdersDTO
    {
        public int Order_Id { get; set; }
        public int Customer_Id { get; set; }
        public DateTime Order_Date { get; set; }
        public decimal Total_Amount { get; set; }
        public Enums.OrderStatus Status { get; set; }  // e.g., "Pending", "Completed", etc.
        public Enums.PaymentMethod Payment_Method { get; set; }  // e.g., "Credit Card", "PayPal", etc.
    }

}
