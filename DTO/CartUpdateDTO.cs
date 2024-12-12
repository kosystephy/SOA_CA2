namespace SOA_CA2_E_Commerce.DTO
{
    public class CartUpdateDTO
    {
        public List<CartItemUpdateDTO> CartItems { get; set; } = new List<CartItemUpdateDTO>();
    }

    public class CartItemUpdateDTO
    {
        public int Product_Id { get; set; }
        public int Quantity { get; set; } 
    }

}
