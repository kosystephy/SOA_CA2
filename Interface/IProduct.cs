using System.Collections.Generic;
using System.Threading.Tasks;
using SOA_CA2_E_Commerce.DTO;

namespace SOA_CA2_E_Commerce.Interface
{
    public interface IProduct
    {
        //Basic CRUD operations
        Task<IEnumerable<ProductsDTO>> GetAllProducts();
        Task<ProductsDTO> GetProductById(int id);
        Task<ProductsDTO> CreateProduct(ProductsDTO product);

        Task<ProductsDTO> UpdateProduct(int id, ProductsDTO product);

        Task DeleteProduct(int id);

        //Custom operations

        Task<IEnumerable<ProductsDTO>> SearchProductsByName(string Product_Name);

        Task<IEnumerable<ProductsDTO>> GetProductsByCategory(int Category_Id);

        Task<bool> IsProductInStock(int Category_Id);


    }
}
