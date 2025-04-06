using FinanceTracker.Models;
using FinanceTracker.ViewModels;

namespace FinanceTracker.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountViewModel>> GetAllAccountsAsync();
        Task<AccountViewModel?> GetAccountByIdAsync(int id);
        Task<AccountViewModel> CreateAccountAsync(AccountViewModel viewModel);
        Task<AccountViewModel> UpdateAccountAsync(AccountViewModel viewModel);
        Task DeleteAccountAsync(int id);
        Task<bool> AccountExistsAsync(int id);
        Task<decimal> GetTotalBalanceAsync();
    }
}