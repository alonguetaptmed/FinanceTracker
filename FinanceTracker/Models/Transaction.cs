using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Models
{
    public enum TransactionType
    {
        [Display(Name = "Débit")]
        Debit,
        [Display(Name = "Crédit")]
        Credit
    }

    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Type")]
        public TransactionType Type { get; set; }

        [Required]
        [Display(Name = "Montant")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le montant doit être supérieur à 0")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Today;

        [Display(Name = "Notes")]
        [StringLength(500)]
        public string? Notes { get; set; }

        [Required]
        [Display(Name = "Compte")]
        public int AccountId { get; set; }
        
        // Navigation properties
        public Account? Account { get; set; }
        
        public ICollection<TransactionCategory> TransactionCategories { get; set; } = new List<TransactionCategory>();
        
        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
        
        // Timestamp pour le suivi des modifications
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; }
    }
}