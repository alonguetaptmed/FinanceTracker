using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Models
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nom du compte")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Solde initial")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal InitialBalance { get; set; } = 0;

        [Display(Name = "Date de création")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        // Computed property
        [Display(Name = "Solde actuel")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal CurrentBalance
        {
            get
            {
                decimal balance = InitialBalance;
                
                if (Transactions != null)
                {
                    foreach (var transaction in Transactions)
                    {
                        if (transaction.Type == TransactionType.Credit)
                        {
                            balance += transaction.Amount;
                        }
                        else
                        {
                            balance -= transaction.Amount;
                        }
                    }
                }
                
                return balance;
            }
        }
    }
}