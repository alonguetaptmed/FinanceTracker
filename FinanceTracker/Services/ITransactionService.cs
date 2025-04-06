using FinanceTracker.Models;
using FinanceTracker.ViewModels;
using Microsoft.AspNetCore.Http;

namespace FinanceTracker.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionViewModel>> GetAllTransactionsAsync();
        Task<IEnumerable<TransactionViewModel>> GetRecentTransactionsAsync(int count);
        Task<IEnumerable<TransactionViewModel>> GetTransactionsByAccountAsync(int accountId);
        Task<IEnumerable<TransactionViewModel>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<TransactionViewModel>> GetTransactionsByCategoryAsync(int categoryId);
        Task<IEnumerable<TransactionViewModel>> GetFilteredTransactionsAsync(int? accountId, DateTime? startDate, DateTime? endDate, TransactionType? type);
        Task<TransactionViewModel?> GetTransactionByIdAsync(int id);
        Task<TransactionViewModel> CreateTransactionAsync(TransactionViewModel viewModel, IFormFileCollection files);
        Task<TransactionViewModel> UpdateTransactionAsync(TransactionViewModel viewModel, IFormFileCollection files);
        Task DeleteTransactionAsync(int id);
        Task<bool> TransactionExistsAsync(int id);
        Task<decimal> GetTotalExpensesAsync(DateTime startDate, DateTime endDate, int? accountId = null);
        Task<decimal> GetTotalIncomeAsync(DateTime startDate, DateTime endDate, int? accountId = null);
    }
}