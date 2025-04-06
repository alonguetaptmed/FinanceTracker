using FinanceTracker.Models;
using FinanceTracker.ViewModels;

namespace FinanceTracker.Services
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboardDataAsync(int? accountId = null, string period = "month", DateTime? startDate = null, DateTime? endDate = null, TransactionType? transactionType = null);
        Task<List<CategoryChartData>> GetExpensesByCategoryAsync(DateTime startDate, DateTime endDate, int? accountId = null);
        Task<List<BalanceChartData>> GetBalanceHistoryAsync(DateTime startDate, DateTime endDate, int? accountId = null);
    }
}