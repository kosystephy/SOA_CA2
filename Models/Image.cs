namespace SOA_CA2_E_Commerce.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public int ProductId { get; set; }
        public string fileName { get; set; }
        public string Link { get; set; }

        public Product Product { get; set; }
    }
}
