using System.ComponentModel.DataAnnotations;

namespace SOA_CA2_E_Commerce.Models
{
    public class Orders
    {
        public int Order_Id { get; set; }

        [Required]
        public int Customer_Id { get; set; }

        [Required]
        public DateTime Order_Date { get; set; }

        [Required]
        public decimal Total_Amount { get; set; }

        [Required]
        public Enums.OrderStatus Status { get; set; } 

        [Required]
        public Enums.PaymentMethod Payment_Method { get; set; }

    public Customers Customer { get; set; }
        public ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
    }
}

 