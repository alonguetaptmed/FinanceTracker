using FinanceTracker.Data;
using FinanceTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.TransactionCategories)
                    .ThenInclude(tc => tc.Category)
                .Include(t => t.Attachments)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetRecentTransactionsAsync(int count)
        {
            return await _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.TransactionCategories)
                    .ThenInclude(tc => tc.Category)
                .Include(t => t.Attachments)
                .OrderByDescending(t => t.Date)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByAccountAsync(int accountId)
        {
            return await _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.TransactionCategories)
                    .ThenInclude(tc => tc.Category)
                .Include(t => t.Attachments)
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.TransactionCategories)
                    .ThenInclude(tc => tc.Category)
                .Include(t => t.Attachments)
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByCategoryAsync(int categoryId)
        {
            return await _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.TransactionCategories)
                    .ThenInclude(tc => tc.Category)
                .Include(t => t.Attachments)
                .Where(t => t.TransactionCategories.Any(tc => tc.CategoryId == categoryId))
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetFilteredTransactionsAsync(int? accountId, DateTime? startDate, DateTime? endDate, TransactionType? type)
        {
            var query = _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.TransactionCategories)
                    .ThenInclude(tc => tc.Category)
                .Include(t => t.Attachments)
                .AsQueryable();

            if (accountId.HasValue && accountId.Value > 0)
            {
                query = query.Where(t => t.AccountId == accountId.Value);
            }

            if (startDate.HasValue)
            {
                query = query.Where(t => t.Date >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(t => t.Date <= endDate.Value);
            }

            if (type.HasValue)
            {
                query = query.Where(t => t.Type == type.Value);
            }

            return await query.OrderByDescending(t => t.Date).ToListAsync();
        }

        public async Task<Transaction?> GetTransactionByIdAsync(int id)
        {
            return await _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.TransactionCategories)
                    .ThenInclude(tc => tc.Category)
                .Include(t => t.Attachments)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Transaction> AddTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<Transaction> UpdateTransactionAsync(Transaction transaction)
        {
            _context.Entry(transaction).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> TransactionExistsAsync(int id)
        {
            return await _context.Transactions.AnyAsync(t => t.Id == id);
        }
    }
}