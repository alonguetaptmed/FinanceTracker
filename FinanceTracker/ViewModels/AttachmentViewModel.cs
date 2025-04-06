using System.ComponentModel.DataAnnotations;
using FinanceTracker.Models;

namespace FinanceTracker.ViewModels
{
    public class AttachmentViewModel
    {
        public int Id { get; set; }
        
        [Display(Name = "Nom du fichier")]
        public string FileName { get; set; } = string.Empty;
        
        [Display(Name = "Chemin du fichier")]
        public string FilePath { get; set; } = string.Empty;
        
        [Display(Name = "Type de fichier")]
        public string ContentType { get; set; } = string.Empty;
        
        [Display(Name = "Taille du fichier")]
        public long FileSize { get; set; }
        
        [Display(Name = "Date d'ajout")]
        public DateTime UploadedAt { get; set; }
        
        public int TransactionId { get; set; }
        
        public bool IsImage => ContentType.StartsWith("image/");
        
        // Format de la taille du fichier pour l'affichage
        public string FormattedSize
        {
            get
            {
                if (FileSize < 1024)
                    return $"{FileSize} B";
                else if (FileSize < 1024 * 1024)
                    return $"{FileSize / 1024:F2} KB";
                else
                    return $"{FileSize / (1024 * 1024):F2} MB";
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
        
        public Attachment ToAttachment()
        {
            return new Attachment
            {
                Id = Id,
                FileName = FileName,
                FilePath = FilePath,
                ContentType = ContentType,
                FileSize = FileSize,
                UploadedAt = UploadedAt,
                TransactionId = TransactionId
            };
        }
    }
}