using FinanceTracker.Models;
using FinanceTracker.Repositories;
using FinanceTracker.ViewModels;

namespace FinanceTracker.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IEnumerable<AccountViewModel>> GetAllAccountsAsync()
        {
            var accounts = await _accountRepository.GetAllAccountsAsync();
            return accounts.Select(AccountViewModel.FromAccount);
        }

        public async Task<AccountViewModel?> GetAccountByIdAsync(int id)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);
            return account != null ? AccountViewModel.FromAccount(account) : null;
        }

        public async Task<AccountViewModel> CreateAccountAsync(AccountViewModel viewModel)
        {
            var account = viewModel.ToAccount();
            await _accountRepository.AddAccountAsync(account);
            return AccountViewModel.FromAccount(account);
        }

        public async Task<AccountViewModel> UpdateAccountAsync(AccountViewModel viewModel)
        {
            var account = viewModel.ToAccount();
            await _accountRepository.UpdateAccountAsync(account);
            return AccountViewModel.FromAccount(account);
        }

        public async Task DeleteAccountAsync(int id)
        {
            await _accountRepository.DeleteAccountAsync(id);
        }

        public async Task<bool> AccountExistsAsync(int id)
        {
            return await _accountRepository.AccountExistsAsync(id);
        }

        public async Task<decimal> GetTotalBalanceAsync()
        {
            var accounts = await _accountRepository.GetAllAccountsAsync();
            decimal totalBalance = 0;

            foreach (var account in accounts)
            {
                totalBalance += account.CurrentBalance;
            }

            return totalBalance;
        }
    }
}