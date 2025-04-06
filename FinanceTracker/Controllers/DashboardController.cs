using FinanceTracker.Models;
using FinanceTracker.Services;
using FinanceTracker.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanceTracker.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        private readonly IAccountService _accountService;

        public DashboardController(IDashboardService dashboardService, IAccountService accountService)
        {
            _dashboardService = dashboardService;
            _accountService = accountService;
        }

        // GET: Dashboard
        public async Task<IActionResult> Index(int? accountId = null, string period = "month", TransactionType? type = null)
        {
            var dashboard = await _dashboardService.GetDashboardDataAsync(accountId, period, null, null, type);
            
            var accounts = await _accountService.GetAllAccountsAsync();
            ViewBag.Accounts = new SelectList(accounts, "Id", "Name");
            
            ViewBag.Periods = new SelectList(new[]
            {
                new { Value = "week", Text = "Cette semaine" },
                new { Value = "month", Text = "Ce mois" },
                new { Value = "quarter", Text = "Ce trimestre" },
                new { Value = "year", Text = "Cette année" },
                new { Value = "all", Text = "Tout" },
            }, "Value", "Text");
            
            ViewBag.TransactionTypes = new SelectList(new[]
            {
                new { Value = "", Text = "Tous" },
                new { Value = ((int)TransactionType.Credit).ToString(), Text = "Crédit" },
                new { Value = ((int)TransactionType.Debit).ToString(), Text = "Débit" },
            }, "Value", "Text");
            
            return View(dashboard);
        }

        // POST: Dashboard/Filter
        [HttpPost]
        public async Task<IActionResult> Filter(DashboardViewModel model)
        {
            return RedirectToAction("Index", new { accountId = model.AccountId, period = model.Period, type = model.TransactionType });
        }

        // GET: Dashboard/ExpensesByCategory
        [HttpGet]
        public async Task<IActionResult> ExpensesByCategory(DateTime startDate, DateTime endDate, int? accountId = null)
        {
            var data = await _dashboardService.GetExpensesByCategoryAsync(startDate, endDate, accountId);
            return Json(data);
        }

        // GET: Dashboard/BalanceHistory
        [HttpGet]
        public async Task<IActionResult> BalanceHistory(DateTime startDate, DateTime endDate, int? accountId = null)
        {
            var data = await _dashboardService.GetBalanceHistoryAsync(startDate, endDate, accountId);
            return Json(data);
        }
    }
}