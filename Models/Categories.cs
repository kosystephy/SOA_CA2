namespace SOA_CA2_E_Commerce.Models
{
    public class Categories
    {
        public int Category_Id { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Products> Products { get; set; } = new List<Products>();
    }
}
