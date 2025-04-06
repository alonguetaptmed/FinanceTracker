using FinanceTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace FinanceTracker.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _uploadsFolder;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            
            // S'assurer que le dossier d'uploads existe
            if (!Directory.Exists(_uploadsFolder))
            {
                Directory.CreateDirectory(_uploadsFolder);
            }
        }

        public async Task<Attachment> SaveFileAsync(IFormFile file, int transactionId)
        {
            // Créer un nom de fichier unique pour éviter les collisions
            string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            string filePath = Path.Combine(_uploadsFolder, uniqueFileName);
            
            // Enregistrer le fichier physiquement
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            
            // Créer l'objet Attachment
            var attachment = new Attachment
            {
                FileName = file.FileName,
                FilePath = $"/uploads/{uniqueFileName}", // Chemin relatif pour l'URL
                ContentType = file.ContentType,
                FileSize = file.Length,
                UploadedAt = DateTime.Now,
                TransactionId = transactionId
            };
            
            return attachment;
        }

        public async Task DeleteFileAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;
            
            // Convertir le chemin relatif en chemin absolu
            string fileName = Path.GetFileName(filePath);
            string fullPath = Path.Combine(_uploadsFolder, fileName);
            
            if (File.Exists(fullPath))
            {
                await Task.Run(() => File.Delete(fullPath));
            }
        }

        public string GetFileUrl(string fileName)
        {
            return $"/uploads/{fileName}";
        }
    }
}