namespace SOA_CA2_E_Commerce.Models
{
    public class Auths
    {
        public int Auth_Id { get; set; } // Primary key
        public int Customer_Id { get; set; } // Foreign key to Customers
        public string Api_Key { get; set; } // The API key
        public DateTime CreatedAt { get; set; } // Key creation date
        public DateTime Expiration { get; set; } // Key expiration date

        // Navigation property
        public Customers Customer { get; set; }
    }
}
