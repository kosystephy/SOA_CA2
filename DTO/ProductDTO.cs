using SOA_CA2_E_Commerce.Enums;
using System.ComponentModel.DataAnnotations;

namespace SOA_CA2_E_Commerce.DTO

{
    public class ProductDTO
    {
        public int Product_Id { get; set; }

        [Required]
        public int Category_Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Product_Name { get; set; }

           [Required]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public GenderType Gender { get; set; } // Using the enum

        [Required]
        [Url(ErrorMessage = "Invalid URL format.")]
        public string ImageUrl { get; set; } // Image link validation


    }

}
