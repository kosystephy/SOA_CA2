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
        public string Status { get; set; } // e.g., "Pending", "Completed", "Cancelled"

        public Customers Customer { get; set; }
        public ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
    }
}
