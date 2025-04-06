using FinanceTracker.Models;
using FinanceTracker.ViewModels;

namespace FinanceTracker.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync();
        Task<CategoryViewModel?> GetCategoryByIdAsync(int id);
        Task<CategoryViewModel> CreateCategoryAsync(CategoryViewModel viewModel);
        Task<CategoryViewModel> UpdateCategoryAsync(CategoryViewModel viewModel);
        Task DeleteCategoryAsync(int id);
        Task<bool> CategoryExistsAsync(int id);
        Task<List<CategoryViewModel>> GetCategoriesWithTransactionCountAsync();
    }
}