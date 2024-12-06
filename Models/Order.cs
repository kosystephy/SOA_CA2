using System.ComponentModel.DataAnnotations;

namespace SOA_CA2_E_Commerce.Models
{
    public class Order
    {
        public int Order_Id { get; set; }

        [Required]
        public int User_Id { get; set; }

        [Required]
        public DateTime Order_Date { get; set; }

        [Required]
        public decimal Total_Amount { get; set; }

        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
        [Required]
        public Enums.OrderStatus Status { get; set; } = Enums.OrderStatus.Pending;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}

 