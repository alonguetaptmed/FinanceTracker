using FinanceTracker.Models;

namespace FinanceTracker.Repositories
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account?> GetAccountByIdAsync(int id);
        Task<Account> AddAccountAsync(Account account);
        Task<Account> UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(int id);
        Task<bool> AccountExistsAsync(int id);
        Task<bool> HasTransactionsAsync(int id);
        Task<decimal> GetCurrentBalanceAsync(int id);
    }
}