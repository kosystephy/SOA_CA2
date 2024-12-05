using Microsoft.EntityFrameworkCore;
using SOA_CA2_E_Commerce.Data;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Models;

namespace SOA_CA2_E_Commerce.Services
{
    public class ProductService : IProduct
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProductDTO>> GetAllProductsAsync()
        {
            return await _dbContext.Products
                .Select(p => new ProductDTO
                {
                    Product_Id = p.Product_Id,
                    Category_Id = p.Category_Id,
                    Product_Name = p.Product_Name,
                    Price = p.Price,
                    Stock = p.Stock,
                    Description = p.Description,
                    Gender = p.Gender,
                    ImageUrl = p.ImageUrl
                }).ToListAsync();
        }

        public async Task<ProductDTO> GetProductByIdAsync(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            if (product == null) return null;

            return new ProductDTO
            {
                Product_Id = product.Product_Id,
                Category_Id = product.Category_Id,
                Product_Name = product.Product_Name,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description,
                Gender = product.Gender,
                ImageUrl = product.ImageUrl
            };
        }

        public async Task<bool> AddProductAsync(ProductDTO productDto)
        {
            var product = new Product
            {
                Category_Id = productDto.Category_Id,
                Product_Name = productDto.Product_Name,
                Price = productDto.Price,
                Stock = productDto.Stock,
                Description = productDto.Description,
                Gender = productDto.Gender,
                ImageUrl = productDto.ImageUrl
            };

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProductAsync(int productId, ProductDTO productDto)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            if (product == null) return false;

            product.Category_Id = productDto.Category_Id;
            product.Product_Name = productDto.Product_Name;
            product.Price = productDto.Price;
            product.Stock = productDto.Stock;
            product.Description = productDto.Description;
            product.Gender = productDto.Gender;
            product.ImageUrl = productDto.ImageUrl;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            if (product == null) return false;

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
