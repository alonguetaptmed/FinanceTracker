using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nom de la catégorie")]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Couleur")]
        [StringLength(7)]
        [RegularExpression("^#([A-Fa-f0-9]{6})$", ErrorMessage = "La couleur doit être au format hexadécimal (ex: #FF5733)")]
        public string Color { get; set; } = "#3498db"; // Couleur par défaut

        // Navigation properties
        public ICollection<TransactionCategory> TransactionCategories { get; set; } = new List<TransactionCategory>();
    }
}