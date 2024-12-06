using Microsoft.EntityFrameworkCore;
using SOA_CA2_E_Commerce.Data;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Models;

namespace SOA_CA2_E_Commerce.Services
{
    public class CategoryService : ICategory
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            return await _dbContext.Categories
                .Select(c => new CategoryDTO
                {
                    Category_Id = c.Category_Id,
                    CategoryName = c.CategoryName
                }).ToListAsync();
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _dbContext.Categories.FindAsync(categoryId);
            if (category == null) return null;

            return new CategoryDTO
            {
                Category_Id = category.Category_Id,
                CategoryName = category.CategoryName
            };
        }

        public async Task<bool> AddCategoryAsync(CategoryDTO categoryDto)
        {
            var category = new Category
            {
                CategoryName = categoryDto.CategoryName
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCategoryAsync(int categoryId, CategoryDTO categoryDto)
        {
            var category = await _dbContext.Categories.FindAsync(categoryId);
            if (category == null) return false;

            category.CategoryName = categoryDto.CategoryName;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var category = await _dbContext.Categories.FindAsync(categoryId);
            if (category == null) return false;

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
