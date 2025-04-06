using FinanceTracker.Models;

namespace FinanceTracker.Services
{
    public interface IFileService
    {
        Task<Attachment> SaveFileAsync(IFormFile file, int transactionId);
        Task DeleteFileAsync(string filePath);
        string GetFileUrl(string fileName);
    }
}