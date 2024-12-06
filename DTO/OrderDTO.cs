using System.ComponentModel.DataAnnotations;

namespace SOA_CA2_E_Commerce.DTO
{
    public class OrderDTO
    {
        public int Order_Id { get; set; }

        [Required]
        public int User_Id { get; set; }

        [Required]
        public DateTime Order_Date { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than zero.")]
        public decimal Total_Amount { get; set; }

        [Required]
        public Enums.OrderStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; }
    }

}