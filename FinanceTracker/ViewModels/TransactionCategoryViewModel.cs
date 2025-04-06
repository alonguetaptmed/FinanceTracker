using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.ViewModels
{
    public class TransactionCategoryViewModel
    {
        public int CategoryId { get; set; }
        
        [Required(ErrorMessage = "Le montant est obligatoire")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le montant doit être supérieur à 0")]
        [Display(Name = "Montant")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }
        
        [Display(Name = "Catégorie")]
        public string CategoryName { get; set; } = string.Empty;
        
        [Display(Name = "Couleur")]
        public string CategoryColor { get; set; } = "#3498db";
        
        [Display(Name = "Pourcentage")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = false)]
        public decimal Percentage { get; set; }
    }
}