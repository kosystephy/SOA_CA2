namespace SOA_CA2_E_Commerce.Models
{
    public class Category
    {
        public int Category_Id { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
