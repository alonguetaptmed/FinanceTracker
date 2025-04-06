using FinanceTracker.Models;

namespace FinanceTracker.Repositories
{
    public interface IAttachmentRepository
    {
        Task<IEnumerable<Attachment>> GetAttachmentsByTransactionIdAsync(int transactionId);
        Task<Attachment?> GetAttachmentByIdAsync(int id);
        Task<Attachment> AddAttachmentAsync(Attachment attachment);
        Task DeleteAttachmentAsync(int id);
        Task<bool> AttachmentExistsAsync(int id);
        Task DeleteAttachmentsByTransactionIdAsync(int transactionId);
    }
}