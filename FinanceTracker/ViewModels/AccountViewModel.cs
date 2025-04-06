using System.ComponentModel.DataAnnotations;
using FinanceTracker.Models;

namespace FinanceTracker.ViewModels
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Le nom du compte est obligatoire")]
        [StringLength(100, ErrorMessage = "Le nom ne doit pas dépasser 100 caractères")]
        [Display(Name = "Nom du compte")]
        public string Name { get; set; } = string.Empty;
        
        [Display(Name = "Solde initial")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal InitialBalance { get; set; } = 0;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // Pour l'affichage du solde actuel
        [Display(Name = "Solde actuel")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal CurrentBalance { get; set; } = 0;
        
        // Pour l'affichage du nombre de transactions associées
        [Display(Name = "Nombre de transactions")]
        public int TransactionCount { get; set; } = 0;
        
        // Convertisseur entre le modèle et le ViewModel
        public static AccountViewModel FromAccount(Account account)
        {
            return new AccountViewModel
            {
                Id = account.Id,
                Name = account.Name,
                InitialBalance = account.InitialBalance,
                CreatedAt = account.CreatedAt,
                CurrentBalance = account.CurrentBalance,
                TransactionCount = account.Transactions?.Count ?? 0
            };
        }
        
        public Account ToAccount()
        {
            return new Account
            {
                Id = Id,
                Name = Name,
                InitialBalance = InitialBalance,
                CreatedAt = CreatedAt
            };
        }
    }
}