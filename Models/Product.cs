using static System.Net.Mime.MediaTypeNames;

namespace SOA_CA2_E_Commerce.Models
{
    public class Product
    {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string Brand { get; set; }
            public string Gender { get; set; }
            public string Category { get; set; }
            public string SubCategory { get; set; }
            public string Season { get; set; }
            public int Year { get; set; }
            public string Usage { get; set; }
            public string Description { get; set; }

        // Navigation Property
        public ICollection<Image> Images { get; set; } = new List<Image>();
        }

    
}
