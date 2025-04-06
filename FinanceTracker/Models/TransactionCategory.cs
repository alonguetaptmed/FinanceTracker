using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Models
{
    public class TransactionCategory
    {
        public int Id { get; set; }
        
        public int TransactionId { get; set; }
        public Transaction? Transaction { get; set; }
        
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        
        [Required]
        [Display(Name = "Montant alloué")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le montant doit être supérieur à 0")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }
        
        // Pourcentage du montant total de la transaction (calculé)
        [Display(Name = "Pourcentage")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = false)]
        public decimal Percentage
        {
            get
            {
                if (Transaction != null && Transaction.Amount > 0)
                {
                    return Amount / Transaction.Amount;
                }
                return 0;
            }
        }
    }
}