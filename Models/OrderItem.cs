using System.ComponentModel.DataAnnotations;

namespace SOA_CA2_E_Commerce.Models
{
    public class OrderItem
    {
        public int OrderItem_Id { get; set; }

        [Required]
        public int Order_Id { get; set; }

        [Required]
        public int Product_Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal TotalPrice => Quantity * Price;

        public Order Order { get; set; }

        public Product Product { get; set; }
    }
}
