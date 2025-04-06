using System.ComponentModel.DataAnnotations;
using FinanceTracker.Models;

namespace FinanceTracker.ViewModels
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Veuillez choisir un type de transaction")]
        [Display(Name = "Type")]
        public TransactionType Type { get; set; }
        
        [Required(ErrorMessage = "Le montant est obligatoire")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le montant doit être supérieur à 0")]
        [Display(Name = "Montant")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }
        
        [Required(ErrorMessage = "La date est obligatoire")]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Today;
        
        [Display(Name = "Notes")]
        [StringLength(500, ErrorMessage = "Les notes ne doivent pas dépasser 500 caractères")]
        public string? Notes { get; set; }
        
        [Required(ErrorMessage = "Veuillez choisir un compte")]
        [Display(Name = "Compte")]
        public int AccountId { get; set; }
        
        [Display(Name = "Nom du compte")]
        public string AccountName { get; set; } = string.Empty;
        
        // Catégories sélectionnées et leurs montants
        [Display(Name = "Catégories")]
        public List<TransactionCategoryViewModel> Categories { get; set; } = new List<TransactionCategoryViewModel>();
        
        // IDs des catégories sélectionnées (pour le formulaire)
        [Required(ErrorMessage = "Veuillez sélectionner au moins une catégorie")]
        [Display(Name = "Catégories")]
        public List<int> SelectedCategoryIds { get; set; } = new List<int>();
        
        // Pour les fichiers joints
        [Display(Name = "Fichiers joints")]
        public List<IFormFile>? UploadedFiles { get; set; }
        
        // Liste des pièces jointes pour l'affichage
        public List<AttachmentViewModel> Attachments { get; set; } = new List<AttachmentViewModel>();
        
        // Date de création pour l'affichage
        [Display(Name = "Créé le")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // Date de modification pour l'affichage
        [Display(Name = "Modifié le")]
        public DateTime? UpdatedAt { get; set; }
        
        // Convertisseur entre le modèle et le ViewModel
        public static TransactionViewModel FromTransaction(Transaction transaction)
        {
            var viewModel = new TransactionViewModel
            {
                Id = transaction.Id,
                Type = transaction.Type,
                Amount = transaction.Amount,
                Date = transaction.Date,
                Notes = transaction.Notes,
                AccountId = transaction.AccountId,
                AccountName = transaction.Account?.Name ?? "",
                CreatedAt = transaction.CreatedAt,
                UpdatedAt = transaction.UpdatedAt
            };
            
            // Récupération des catégories
            if (transaction.TransactionCategories != null)
            {
                foreach (var tc in transaction.TransactionCategories)
                {
                    viewModel.Categories.Add(new TransactionCategoryViewModel
                    {
                        CategoryId = tc.CategoryId,
                        CategoryName = tc.Category?.Name ?? "",
                        CategoryColor = tc.Category?.Color ?? "#3498db",
                        Amount = tc.Amount,
                        Percentage = tc.Percentage
                    });
                    
                    viewModel.SelectedCategoryIds.Add(tc.CategoryId);
                }
            }
            
            // Récupération des pièces jointes
            if (transaction.Attachments != null)
            {
                foreach (var attachment in transaction.Attachments)
                {
                    viewModel.Attachments.Add(AttachmentViewModel.FromAttachment(attachment));
                }
            }
            
            return viewModel;
        }
        
        public Transaction ToTransaction()
        {
            return new Transaction
            {
                Id = Id,
                Type = Type,
                Amount = Amount,
                Date = Date,
                Notes = Notes,
                AccountId = AccountId,
                CreatedAt = CreatedAt,
                UpdatedAt = DateTime.Now
            };
        }
    }
}