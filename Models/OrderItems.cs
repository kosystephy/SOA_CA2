using System.ComponentModel.DataAnnotations;

namespace SOA_CA2_E_Commerce.Models
{
    public class OrderItems
    {
        public int Item_Id { get; set; }

        [Required]
        public int Order_Id { get; set; }

        [Required]
        public int Product_Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        public Orders Order { get; set; }

        public Product Product { get; set; }
    }
}
