using SOA_CA2_E_Commerce.Data;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Interface;
using Microsoft.EntityFrameworkCore;
using SOA_CA2_E_Commerce.Models;

namespace SOA_CA2_E_Commerce.Services
{
    public class CategoryService : ICategory
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            return await _context.Categories
                .Select(c => new CategoryDTO
                {
                    Category_Id = c.Category_Id,
                    CategoryName = c.CategoryName
                }).ToListAsync();
        }

        public async Task<CategoryDTO> GetCategoryById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) throw new KeyNotFoundException("Category not found");

            return new CategoryDTO
            {
                Category_Id = category.Category_Id,
                CategoryName = category.CategoryName
            };
        }

        public async Task<CategoryDTO> CreateCategory(CategoryDTO categoryDTO)
        {
            var category = new Category
            {
                CategoryName = categoryDTO.CategoryName
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            categoryDTO.Category_Id = category.Category_Id;
            return categoryDTO;
        }

        public async Task<CategoryDTO> UpdateCategory(int id, CategoryDTO categoryDTO)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) throw new KeyNotFoundException("Category not found");

            category.CategoryName = categoryDTO.CategoryName;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return categoryDTO;
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) throw new KeyNotFoundException("Category not found");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
