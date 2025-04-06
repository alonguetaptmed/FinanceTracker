using FinanceTracker.Models;
using FinanceTracker.Repositories;
using FinanceTracker.ViewModels;

namespace FinanceTracker.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return categories.Select(CategoryViewModel.FromCategory);
        }

        public async Task<CategoryViewModel?> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            return category != null ? CategoryViewModel.FromCategory(category) : null;
        }

        public async Task<CategoryViewModel> CreateCategoryAsync(CategoryViewModel viewModel)
        {
            var category = viewModel.ToCategory();
            await _categoryRepository.AddCategoryAsync(category);
            return CategoryViewModel.FromCategory(category);
        }

        public async Task<CategoryViewModel> UpdateCategoryAsync(CategoryViewModel viewModel)
        {
            var category = viewModel.ToCategory();
            await _categoryRepository.UpdateCategoryAsync(category);
            return CategoryViewModel.FromCategory(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
        }

        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await _categoryRepository.CategoryExistsAsync(id);
        }

        public async Task<List<CategoryViewModel>> GetCategoriesWithTransactionCountAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return categories.Select(CategoryViewModel.FromCategory).ToList();
        }
    }
}