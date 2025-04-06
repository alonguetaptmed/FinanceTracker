using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.ViewModels
{
    public class DashboardViewModel
    {
        // Filtres pour le tableau de bord
        [Display(Name = "Compte")]
        public int? AccountId { get; set; }
        
        [Display(Name = "Type")]
        public string? TransactionType { get; set; }
        
        [Display(Name = "Du")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        
        [Display(Name = "Au")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        
        // Données de statistiques
        [Display(Name = "Solde total")]
        public decimal TotalBalance { get; set; }
        
        [Display(Name = "Total des revenus")]
        public decimal TotalIncome { get; set; }
        
        [Display(Name = "Total des dépenses")]
        public decimal TotalExpenses { get; set; }
        
        // Statistiques par catégorie
        public List<CategoryStatViewModel> CategoryStats { get; set; } = new List<CategoryStatViewModel>();
        
        // Statistiques mensuelles
        public List<MonthlyStatViewModel> MonthlyStats { get; set; } = new List<MonthlyStatViewModel>();
        
        // Soldes des comptes
        public List<AccountBalanceViewModel> AccountBalances { get; set; } = new List<AccountBalanceViewModel>();
        
        // Transactions récentes
        public List<TransactionViewModel> RecentTransactions { get; set; } = new List<TransactionViewModel>();
    }
    
    public class CategoryStatViewModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = "#3498db";
        public decimal Amount { get; set; }
        public decimal Percentage { get; set; }
    }
    
    public class MonthlyStatViewModel
    {
        public string Month { get; set; } = string.Empty;
        public decimal Income { get; set; }
        public decimal Expenses { get; set; }
        public decimal Balance => Income - Expenses;
    }
    
    public class AccountBalanceViewModel
    {
        public int AccountId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal InitialBalance { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}