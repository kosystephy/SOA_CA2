using System.Collections.Generic;
using System.Threading.Tasks;
using SOA_CA2_E_Commerce.DTO;

namespace SOA_CA2_E_Commerce.Interface
{
    public interface IProduct
    {
        Task<IEnumerable<ProductDTO>> GetAllProducts();
        Task<ProductDTO> GetProductById(int id);
        Task<ProductDTO> CreateProduct(ProductDTO productDTO);
        Task<ProductDTO> UpdateProduct(int id, ProductDTO productDTO);
        Task DeleteProduct(int id);
        Task<IEnumerable<ProductDTO>> GetProductsByCategory(int categoryId);
    


    }
}
