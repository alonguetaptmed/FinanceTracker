using System.ComponentModel.DataAnnotations;
using FinanceTracker.Models;

namespace FinanceTracker.ViewModels
{
    public class DashboardViewModel
    {
        // Période sélectionnée
        [Display(Name = "Période")]
        public string Period { get; set; } = "month"; // month, quarter, year
        
        // Date de début pour le filtre
        [Display(Name = "Date de début")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        
        // Date de fin pour le filtre
        [Display(Name = "Date de fin")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        
        // Compte sélectionné pour le filtre (0 = tous les comptes)
        [Display(Name = "Compte")]
        public int AccountId { get; set; }
        
        // Type de transaction pour le filtre (null = tous les types)
        [Display(Name = "Type de transaction")]
        public TransactionType? TransactionType { get; set; }
        
        // Liste des comptes disponibles
        public List<AccountViewModel> Accounts { get; set; } = new List<AccountViewModel>();
        
        // Solde total actuel
        [Display(Name = "Solde total")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal TotalBalance { get; set; }
        
        // Total des dépenses pour la période sélectionnée
        [Display(Name = "Dépenses de la période")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal TotalExpenses { get; set; }
        
        // Total des revenus pour la période sélectionnée
        [Display(Name = "Revenus de la période")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal TotalIncome { get; set; }
        
        // Données pour le graphique des dépenses par catégorie
        public List<CategoryChartData> ExpensesByCategory { get; set; } = new List<CategoryChartData>();
        
        // Données pour le graphique de l'évolution des soldes
        public List<BalanceChartData> BalanceHistory { get; set; } = new List<BalanceChartData>();
        
        // Transactions récentes
        public List<TransactionViewModel> RecentTransactions { get; set; } = new List<TransactionViewModel>();
    }
    
    // Classe pour les données du graphique par catégorie
    public class CategoryChartData
    {
        public string CategoryName { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal Percentage { get; set; }
    }
    
    // Classe pour les données du graphique d'évolution des soldes
    public class BalanceChartData
    {
        public string Date { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }
}