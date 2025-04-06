using FinanceTracker.Models;

namespace FinanceTracker.Repositories
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task<IEnumerable<Transaction>> GetRecentTransactionsAsync(int count);
        Task<IEnumerable<Transaction>> GetTransactionsByAccountAsync(int accountId);
        Task<IEnumerable<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Transaction>> GetTransactionsByCategoryAsync(int categoryId);
        Task<IEnumerable<Transaction>> GetFilteredTransactionsAsync(int? accountId, DateTime? startDate, DateTime? endDate, TransactionType? type);
        Task<Transaction?> GetTransactionByIdAsync(int id);
        Task<Transaction> AddTransactionAsync(Transaction transaction);
        Task<Transaction> UpdateTransactionAsync(Transaction transaction);
        Task DeleteTransactionAsync(int id);
        Task<bool> TransactionExistsAsync(int id);
    }
}