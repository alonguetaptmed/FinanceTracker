using FinanceTracker.Models;
using FinanceTracker.Repositories;
using FinanceTracker.ViewModels;

namespace FinanceTracker.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountService _accountService;

        public DashboardService(
            ITransactionRepository transactionRepository,
            IAccountRepository accountRepository,
            IAccountService accountService)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _accountService = accountService;
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync(int? accountId = null, string period = "month", DateTime? startDate = null, DateTime? endDate = null, TransactionType? transactionType = null)
        {
            // Initialiser les dates si non fournies
            DateTime start, end;
            
            if (startDate.HasValue && endDate.HasValue)
            {
                start = startDate.Value;
                end = endDate.Value;
            }
            else
            {
                // Définir les dates selon la période
                (start, end) = GetDateRangeFromPeriod(period);
            }
            
            // Récupérer les comptes
            var accounts = await _accountService.GetAllAccountsAsync();
            
            // Calculer le solde total
            decimal totalBalance = await _accountService.GetTotalBalanceAsync();
            
            // Récupérer les transactions filtrées
            var transactions = await _transactionRepository.GetFilteredTransactionsAsync(accountId, start, end, transactionType);
            
            // Calculer les totaux
            decimal totalExpenses = transactions
                .Where(t => t.Type == TransactionType.Debit)
                .Sum(t => t.Amount);
                
            decimal totalIncome = transactions
                .Where(t => t.Type == TransactionType.Credit)
                .Sum(t => t.Amount);
            
            // Récupérer les transactions récentes
            var recentTransactions = await _transactionRepository.GetRecentTransactionsAsync(5);
            
            // Créer le modèle de vue du tableau de bord
            var dashboardViewModel = new DashboardViewModel
            {
                Period = period,
                StartDate = start,
                EndDate = end,
                AccountId = accountId ?? 0,
                TransactionType = transactionType,
                Accounts = accounts.ToList(),
                TotalBalance = totalBalance,
                TotalExpenses = totalExpenses,
                TotalIncome = totalIncome,
                RecentTransactions = recentTransactions.Select(TransactionViewModel.FromTransaction).ToList()
            };
            
            // Récupérer les données pour le graphique des dépenses par catégorie
            dashboardViewModel.ExpensesByCategory = await GetExpensesByCategoryAsync(start, end, accountId);
            
            // Récupérer les données pour le graphique de l'évolution des soldes
            dashboardViewModel.BalanceHistory = await GetBalanceHistoryAsync(start, end, accountId);
            
            return dashboardViewModel;
        }

        public async Task<List<CategoryChartData>> GetExpensesByCategoryAsync(DateTime startDate, DateTime endDate, int? accountId = null)
        {
            // Récupérer toutes les transactions de type débit pour la période donnée
            var transactions = await _transactionRepository.GetFilteredTransactionsAsync(
                accountId, startDate, endDate, TransactionType.Debit);
            
            // Regrouper les dépenses par catégorie
            var categoryData = new Dictionary<int, CategoryChartData>();
            decimal totalExpenses = 0;
            
            foreach (var transaction in transactions)
            {
                foreach (var tc in transaction.TransactionCategories)
                {
                    if (!categoryData.ContainsKey(tc.CategoryId))
                    {
                        categoryData[tc.CategoryId] = new CategoryChartData
                        {
                            CategoryName = tc.Category?.Name ?? "Inconnu",
                            Color = tc.Category?.Color ?? "#cccccc",
                            Amount = 0
                        };
                    }
                    
                    categoryData[tc.CategoryId].Amount += tc.Amount;
                    totalExpenses += tc.Amount;
                }
            }
            
            // Calculer les pourcentages
            if (totalExpenses > 0)
            {
                foreach (var category in categoryData.Values)
                {
                    category.Percentage = category.Amount / totalExpenses;
                }
            }
            
            return categoryData.Values.OrderByDescending(c => c.Amount).ToList();
        }

        public async Task<List<BalanceChartData>> GetBalanceHistoryAsync(DateTime startDate, DateTime endDate, int? accountId = null)
        {
            // Déterminer l'intervalle de dates selon la durée
            var result = new List<BalanceChartData>();
            var currentDate = new DateTime(startDate.Year, startDate.Month, 1);
            var endMonth = new DateTime(endDate.Year, endDate.Month, 1);
            
            // Si la période est inférieure à un mois, utiliser un intervalle quotidien
            bool dailyInterval = (endDate - startDate).TotalDays <= 31;
            
            // Récupérer les comptes concernés
            IEnumerable<Account> accounts;
            if (accountId.HasValue && accountId.Value > 0)
            {
                var account = await _accountRepository.GetAccountByIdAsync(accountId.Value);
                accounts = account != null ? new List<Account> { account } : new List<Account>();
            }
            else
            {
                accounts = await _accountRepository.GetAllAccountsAsync();
            }
            
            // Pour l'intervalle mensuel
            if (!dailyInterval)
            {
                while (currentDate <= endMonth)
                {
                    decimal balance = 0;
                    var nextMonth = currentDate.AddMonths(1);
                    
                    foreach (var account in accounts)
                    {
                        // Calculer le solde initial jusqu'à cette date
                        decimal initialBalance = account.InitialBalance;
                        var previousTransactions = account.Transactions
                            .Where(t => t.Date < currentDate)
                            .ToList();
                            
                        foreach (var transaction in previousTransactions)
                        {
                            if (transaction.Type == TransactionType.Credit)
                                initialBalance += transaction.Amount;
                            else
                                initialBalance -= transaction.Amount;
                        }
                        
                        balance += initialBalance;
                    }
                    
                    result.Add(new BalanceChartData
                    {
                        Date = currentDate.ToString("MMM yyyy"),
                        Balance = balance
                    });
                    
                    currentDate = nextMonth;
                }
            }
            // Pour l'intervalle quotidien
            else
            {
                var current = startDate.Date;
                while (current <= endDate.Date)
                {
                    decimal balance = 0;
                    
                    foreach (var account in accounts)
                    {
                        // Calculer le solde initial jusqu'à cette date
                        decimal initialBalance = account.InitialBalance;
                        var previousTransactions = account.Transactions
                            .Where(t => t.Date <= current)
                            .ToList();
                            
                        foreach (var transaction in previousTransactions)
                        {
                            if (transaction.Type == TransactionType.Credit)
                                initialBalance += transaction.Amount;
                            else
                                initialBalance -= transaction.Amount;
                        }
                        
                        balance += initialBalance;
                    }
                    
                    result.Add(new BalanceChartData
                    {
                        Date = current.ToString("dd MMM"),
                        Balance = balance
                    });
                    
                    current = current.AddDays(1);
                }
            }
            
            return result;
        }

        private (DateTime start, DateTime end) GetDateRangeFromPeriod(string period)
        {
            DateTime now = DateTime.Now;
            DateTime start, end;
            
            switch (period.ToLower())
            {
                case "week":
                    // Du lundi de cette semaine au dimanche
                    start = now.AddDays(-(int)now.DayOfWeek + 1); // Lundi
                    if (start > now) start = start.AddDays(-7); // Si on est dimanche, prendre la semaine précédente
                    end = start.AddDays(6); // Dimanche
                    break;
                    
                case "month":
                    // Du premier jour du mois au dernier jour du mois
                    start = new DateTime(now.Year, now.Month, 1);
                    end = start.AddMonths(1).AddDays(-1);
                    break;
                    
                case "quarter":
                    // Du premier jour du trimestre au dernier jour du trimestre
                    int currentQuarter = (now.Month - 1) / 3 + 1;
                    start = new DateTime(now.Year, (currentQuarter - 1) * 3 + 1, 1);
                    end = start.AddMonths(3).AddDays(-1);
                    break;
                    
                case "year":
                    // Du premier jour de l'année au dernier jour de l'année
                    start = new DateTime(now.Year, 1, 1);
                    end = new DateTime(now.Year, 12, 31);
                    break;
                    
                default: // "all" ou autre
                    // Du premier janvier 1900 à aujourd'hui
                    start = new DateTime(1900, 1, 1);
                    end = now;
                    break;
            }
            
            return (start, end);
        }
    }
}