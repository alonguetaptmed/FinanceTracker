using FinanceTracker.Data;
using FinanceTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Repositories
{
    public class TransactionCategoryRepository : ITransactionCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransactionCategory>> GetTransactionCategoriesByTransactionIdAsync(int transactionId)
        {
            return await _context.TransactionCategories
                .Include(tc => tc.Category)
                .Where(tc => tc.TransactionId == transactionId)
                .ToListAsync();
        }

        public async Task<TransactionCategory?> GetTransactionCategoryByIdAsync(int id)
        {
            return await _context.TransactionCategories
                .Include(tc => tc.Category)
                .Include(tc => tc.Transaction)
                .FirstOrDefaultAsync(tc => tc.Id == id);
        }

        public async Task<TransactionCategory> AddTransactionCategoryAsync(TransactionCategory transactionCategory)
        {
            _context.TransactionCategories.Add(transactionCategory);
            await _context.SaveChangesAsync();
            return transactionCategory;
        }

        public async Task<TransactionCategory> UpdateTransactionCategoryAsync(TransactionCategory transactionCategory)
        {
            _context.Entry(transactionCategory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return transactionCategory;
        }

        public async Task DeleteTransactionCategoryAsync(int id)
        {
            var transactionCategory = await _context.TransactionCategories.FindAsync(id);
            if (transactionCategory != null)
            {
                _context.TransactionCategories.Remove(transactionCategory);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteTransactionCategoriesByTransactionIdAsync(int transactionId)
        {
            var transactionCategories = await _context.TransactionCategories
                .Where(tc => tc.TransactionId == transactionId)
                .ToListAsync();

            if (transactionCategories.Any())
            {
                _context.TransactionCategories.RemoveRange(transactionCategories);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> TransactionCategoryExistsAsync(int id)
        {
            return await _context.TransactionCategories.AnyAsync(tc => tc.Id == id);
        }
    }
}