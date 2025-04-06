using System.ComponentModel.DataAnnotations;
using FinanceTracker.Models;

namespace FinanceTracker.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Le nom de la catégorie est obligatoire")]
        [StringLength(50, ErrorMessage = "Le nom ne doit pas dépasser 50 caractères")]
        [Display(Name = "Nom de la catégorie")]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(200, ErrorMessage = "La description ne doit pas dépasser 200 caractères")]
        [Display(Name = "Description")]
        public string? Description { get; set; }
        
        [Display(Name = "Couleur")]
        [StringLength(7)]
        [RegularExpression("^#([A-Fa-f0-9]{6})$", ErrorMessage = "La couleur doit être au format hexadécimal (ex: #FF5733)")]
        public string Color { get; set; } = "#3498db"; // Couleur par défaut
        
        // Pour l'affichage du nombre de transactions associées
        [Display(Name = "Nombre de transactions")]
        public int TransactionCount { get; set; } = 0;
        
        // Pour l'affichage du montant total associé à cette catégorie
        [Display(Name = "Montant total")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal TotalAmount { get; set; } = 0;
        
        // Convertisseur entre le modèle et le ViewModel
        public static CategoryViewModel FromCategory(Category category)
        {
            var viewModel = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Color = category.Color,
                TransactionCount = category.TransactionCategories?.Count ?? 0
            };
            
            // Calcul du montant total associé à cette catégorie
            if (category.TransactionCategories != null)
            {
                decimal total = 0;
                foreach (var tc in category.TransactionCategories)
                {
                    total += tc.Amount;
                }
                viewModel.TotalAmount = total;
            }
            
            return viewModel;
        }
        
        public Category ToCategory()
        {
            return new Category
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Color = Color
            };
        }
    }
}