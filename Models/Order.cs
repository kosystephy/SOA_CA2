using System.ComponentModel.DataAnnotations;

namespace SOA_CA2_E_Commerce.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public string Status { get; set; } // e.g., "Pending", "Completed", "Cancelled"

        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
