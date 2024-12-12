using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations;
using SOA_CA2_E_Commerce.Enums;


namespace SOA_CA2_E_Commerce.Models
{
    public class Product
    {
        public int Product_Id { get; set; }

        [Required]
        public int Category_Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string Product_Name { get; set; }

       
        [Required]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
        public int Stock { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required]
        public GenderType Gender { get; set; } 

        [Required]
        [Url(ErrorMessage = "Invalid URL format.")]
        public string ImageUrl { get; set; } 


    
        public Category Category { get; set; }

     
    }



}

