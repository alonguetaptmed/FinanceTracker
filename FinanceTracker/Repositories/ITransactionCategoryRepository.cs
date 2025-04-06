using FinanceTracker.Models;

namespace FinanceTracker.Repositories
{
    public interface ITransactionCategoryRepository
    {
        Task<IEnumerable<TransactionCategory>> GetTransactionCategoriesByTransactionIdAsync(int transactionId);
        Task<TransactionCategory?> GetTransactionCategoryByIdAsync(int id);
        Task<TransactionCategory> AddTransactionCategoryAsync(TransactionCategory transactionCategory);
        Task<TransactionCategory> UpdateTransactionCategoryAsync(TransactionCategory transactionCategory);
        Task DeleteTransactionCategoryAsync(int id);
        Task<bool> TransactionCategoryExistsAsync(int id);
        Task DeleteTransactionCategoriesByTransactionIdAsync(int transactionId);
    }
}