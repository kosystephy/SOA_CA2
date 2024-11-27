using static System.Net.Mime.MediaTypeNames;

namespace SOA_CA2_E_Commerce.Models
{
    public class Product
    {


            public int Product_Id { get; set; }
        public int Category_Id { get; set; }
        public string Product_Name { get; set; }
            public string Brand { get; set; }
            public string Gender { get; set; }
        public int Stock { get; set; }
        public int Year { get; set; }
            public string Description { get; set; }
            public string Image { get; set; }
            public Categories Category { get; set; }
    }

    
}
