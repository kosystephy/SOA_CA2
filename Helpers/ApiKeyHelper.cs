using SOA_CA2_E_Commerce.Models;
using SOA_CA2_E_Commerce.Helpers;
using System.Text;
using System.Security.Cryptography;


namespace SOA_CA2_E_Commerce.Helpers
{
    public static class ApiKeyHelper
    {
        public static string GenerateApiKey()
        {
            using var hmac = new HMACSHA256();
            return Convert.ToBase64String(hmac.Key);
        }

        public static string HashApiKey(string apiKey)
        {
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(apiKey));
            return Convert.ToBase64String(hashBytes);
        }

        public static bool ValidateApiKey(string apiKey, DateTime? expirationDate)
        {
            return expirationDate.HasValue && expirationDate > DateTime.UtcNow;
        }
    }


}
