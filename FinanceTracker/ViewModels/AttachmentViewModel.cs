using System.ComponentModel.DataAnnotations;
using FinanceTracker.Models;

namespace FinanceTracker.ViewModels
{
    public class AttachmentViewModel
    {
        public int Id { get; set; }
        
        [Display(Name = "Nom du fichier")]
        public string FileName { get; set; } = string.Empty;
        
        [Display(Name = "Chemin")]
        public string FilePath { get; set; } = string.Empty;
        
        [Display(Name = "Type")]
        public string ContentType { get; set; } = string.Empty;
        
        [Display(Name = "Taille")]
        public long FileSize { get; set; }
        
        [Display(Name = "Date d'ajout")]
        public DateTime UploadedAt { get; set; }
        
        public int TransactionId { get; set; }
        
        // Pour déterminer s'il s'agit d'une image (pour l'affichage)
        public bool IsImage => ContentType.StartsWith("image/");
        
        // Pour l'affichage formaté de la taille du fichier
        public string FormattedSize
        {
            get
            {
                if (FileSize < 1024)
                    return $"{FileSize} octets";
                if (FileSize < 1024 * 1024)
                    return $"{FileSize / 1024:F2} Ko";
                return $"{FileSize / (1024 * 1024):F2} Mo";
            }
        }
        
        // Convertisseur entre le modèle et le ViewModel
        public static AttachmentViewModel FromAttachment(Attachment attachment)
        {
            return new AttachmentViewModel
            {
                Id = attachment.Id,
                FileName = attachment.FileName,
                FilePath = attachment.FilePath,
                ContentType = attachment.ContentType,
                FileSize = attachment.FileSize,
                UploadedAt = attachment.UploadedAt,
                TransactionId = attachment.TransactionId
            };
        }
    }
}