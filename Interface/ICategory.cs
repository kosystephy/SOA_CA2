using SOA_CA2_E_Commerce.DTO;

namespace SOA_CA2_E_Commerce.Services
{
    public interface ICategory
    {
        Task<List<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(int categoryId);
        Task<bool> AddCategoryAsync(CategoryDTO categoryDto);
        Task<bool> UpdateCategoryAsync(int categoryId, CategoryDTO categoryDto);
        Task<bool> DeleteCategoryAsync(int categoryId);
    }
}
