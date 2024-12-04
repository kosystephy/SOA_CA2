namespace SOA_CA2_E_Commerce.DTO
{
    public class RegisterDTO
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Enums.UserRole Role { get; set; } = Enums.UserRole.Customer;
        public string Address { get; set; }
    
    }
}
