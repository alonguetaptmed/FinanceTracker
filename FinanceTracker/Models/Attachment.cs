using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Nom du fichier")]
        [StringLength(255)]
        public string FileName { get; set; } = string.Empty;
        
        [Required]
        [Display(Name = "Chemin du fichier")]
        [StringLength(255)]
        public string FilePath { get; set; } = string.Empty;
        
        [Display(Name = "Type de fichier")]
        [StringLength(100)]
        public string ContentType { get; set; } = string.Empty;
        
        [Display(Name = "Taille du fichier")]
        public long FileSize { get; set; }
        
        [Display(Name = "Date d'ajout")]
        public DateTime UploadedAt { get; set; } = DateTime.Now;
        
        // Relation avec Transaction
        public int TransactionId { get; set; }
        public Transaction? Transaction { get; set; }
        
        // Propriété pour déterminer si l'attachement est une image
        public bool IsImage => ContentType.StartsWith("image/");
    }
}