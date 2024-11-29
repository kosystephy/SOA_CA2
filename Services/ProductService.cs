using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SOA_CA2_E_Commerce.Data;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Interface;
using SOA_CA2_E_Commerce.Models;
using static System.Net.Mime.MediaTypeNames;

namespace SOA_CA2_E_Commerce.Services
{
    public class ProductService : IProduct
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductsDTO>> GetAllProducts()
        {
            return await _context.Products
                .Select(p => new ProductsDTO
                {
                    Product_Id = p.Product_Id,
                    Product_Name = p.Product_Name,
                    Brand = p.Brand,
                    Price = p.Price,
                    Stock = p.Stock,
                    Category_Id = p.Category_Id,
                    Description = p.Description,
                    Gender = p.Gender,
                    Image = string.IsNullOrEmpty(p.Image) ? "default.jpg" : p.Image,
                })
                .ToListAsync();
        }

        public async Task<ProductsDTO> GetProductById(int id)
        {

            var product = await _context.Products.FindAsync(id);

            if (product == null)
                throw new KeyNotFoundException("Product not found");

            return new ProductsDTO
            {
                Product_Id = product.Product_Id,
                Product_Name = product.Product_Name,
                Brand = product.Brand,
                Price = product.Price,
                Stock = product.Stock,
                Category_Id = product.Category_Id,
                Description = product.Description,
                Gender = product.Gender,
                Image = string.IsNullOrEmpty(product.Image) ? "default.jpg" : product.Image
            };
        }

        public async Task<ProductsDTO> CreateProduct(ProductsDTO productDTO)
        {
            var product = new Products
            {
                Product_Name = productDTO.Product_Name,
                Brand = productDTO.Brand,
                Price = productDTO.Price,
                Stock = productDTO.Stock,
                Category_Id = productDTO.Category_Id,
                Description = productDTO.Description,
                Gender = productDTO.Gender,
                Image = string.IsNullOrEmpty(productDTO.Image) ? "default.jpg" : productDTO.Image
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            productDTO.Product_Id = product.Product_Id;
            return productDTO;
        }

        public async Task<ProductsDTO> UpdateProduct(int id, ProductsDTO productDTO)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                throw new KeyNotFoundException("Product not found");

            product.Product_Name = productDTO.Product_Name;
            product.Brand = productDTO.Brand;
            product.Price = productDTO.Price;
            product.Stock = productDTO.Stock;
            product.Category_Id = productDTO.Category_Id;
            product.Description = productDTO.Description;
            product.Gender = productDTO.Gender;
            product.Image = string.IsNullOrEmpty(productDTO.Image) ? "default.jpg" : productDTO.Image;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return productDTO;
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                throw new KeyNotFoundException("Product not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductsDTO>> SearchProductsByName(string Product_Name)
        {
            return await _context.Products
                .Where(p => p.Product_Name.Contains(Product_Name, StringComparison.OrdinalIgnoreCase))
                .Select(p => new ProductsDTO
                {
                    Product_Id = p.Product_Id,
                    Product_Name = p.Product_Name,
                    Brand = p.Brand,
                    Price = p.Price,
                    Stock = p.Stock,
                    Category_Id = p.Category_Id,
                    Description = p.Description,
                    Gender = p.Gender,
                     Image = string.IsNullOrEmpty(p.Image) ? "default.jpg" : p.Image
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductsDTO>> GetProductsByCategory(int Category_Id)
        {
            return await _context.Products
                .Where(p => p.Category_Id == Category_Id)
                .Select(p => new ProductsDTO
                {
                    Product_Id = p.Product_Id,
                    Product_Name = p.Product_Name,
                    Brand = p.Brand,
                    Price = p.Price,
                    Stock = p.Stock,
                    Category_Id = p.Category_Id,
                    Description = p.Description,
                    Gender = p.Gender,
                    Image = string.IsNullOrEmpty(p.Image) ? "default.jpg" : p.Image

                })
                .ToListAsync();
        }

        public async Task<bool> IsProductInStock(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Product not found");

            return product.Stock > 0;
        }
    }
}

