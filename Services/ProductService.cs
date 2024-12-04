using SOA_CA2_E_Commerce.Data;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Interface;
using Microsoft.EntityFrameworkCore;
using SOA_CA2_E_Commerce.Models;

namespace SOA_CA2_E_Commerce.Services
{
    public class ProductService : IProduct
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            return await _context.Products
                .Select(p => new ProductDTO
                {
                    Product_Id = p.Product_Id,
                    Product_Name = p.Product_Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    Gender = p.Gender,
                    ImageUrl = p.ImageUrl,
                    Category_Id = p.Category_Id
                }).ToListAsync();
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) throw new KeyNotFoundException("Product not found");

            return new ProductDTO
            {
                Product_Id = product.Product_Id,
                Product_Name = product.Product_Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Gender = product.Gender,
                ImageUrl = product.ImageUrl,
                Category_Id = product.Category_Id
            };
        }

        public async Task<ProductDTO> CreateProduct(ProductDTO productDTO)
        {
            var product = new Product
            {
                Product_Name = productDTO.Product_Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                Stock = productDTO.Stock,
                Gender = productDTO.Gender,
                ImageUrl = productDTO.ImageUrl,
                Category_Id = productDTO.Category_Id
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            productDTO.Product_Id = product.Product_Id;
            return productDTO;
        }

        public async Task<ProductDTO> UpdateProduct(int id, ProductDTO productDTO)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) throw new KeyNotFoundException("Product not found");

            product.Product_Name = productDTO.Product_Name;
            product.Description = productDTO.Description;
            product.Price = productDTO.Price;
            product.Stock = productDTO.Stock;
            product.Gender = productDTO.Gender;
            product.ImageUrl = productDTO.ImageUrl;
            product.Category_Id = productDTO.Category_Id;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return productDTO;
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) throw new KeyNotFoundException("Product not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByCategory(int categoryId)
        {
            return await _context.Products
                .Where(p => p.Category_Id == categoryId)
                .Select(p => new ProductDTO
                {
                    Product_Id = p.Product_Id,
                    Product_Name = p.Product_Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    Gender = p.Gender,
                    ImageUrl = p.ImageUrl,
                    Category_Id = p.Category_Id
                }).ToListAsync();
        }
    }
}
