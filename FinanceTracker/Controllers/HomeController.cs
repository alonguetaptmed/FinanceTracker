using FinanceTracker.Services;
using FinanceTracker.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinanceTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDashboardService _dashboardService;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        public HomeController(
            IDashboardService dashboardService,
            IAccountService accountService,
            ITransactionService transactionService)
        {
            _dashboardService = dashboardService;
            _accountService = accountService;
            _transactionService = transactionService;
        }

        public async Task<IActionResult> Index()
        {
            // Récupérer les données du tableau de bord pour le mois en cours
            var dashboardData = await _dashboardService.GetDashboardDataAsync();
            return View(dashboardData);
        }

        [HttpPost]
        public async Task<IActionResult> Index(DashboardViewModel model)
        {
            // Récupérer les données du tableau de bord avec les filtres appliqués
            var dashboardData = await _dashboardService.GetDashboardDataAsync(
                model.AccountId == 0 ? null : model.AccountId,
                model.Period,
                model.StartDate,
                model.EndDate,
                model.TransactionType);
                
            return View(dashboardData);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}