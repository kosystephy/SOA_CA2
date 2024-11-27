using System.ComponentModel.DataAnnotations;

namespace SOA_CA2_E_Commerce.Models
{
    public class OrderItem
    {
        public int ItemId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }
    }
}
