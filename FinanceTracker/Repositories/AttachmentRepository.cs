using FinanceTracker.Data;
using FinanceTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Repositories
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AttachmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Attachment>> GetAttachmentsByTransactionIdAsync(int transactionId)
        {
            return await _context.Attachments
                .Where(a => a.TransactionId == transactionId)
                .ToListAsync();
        }

        public async Task<Attachment?> GetAttachmentByIdAsync(int id)
        {
            return await _context.Attachments
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Attachment> AddAttachmentAsync(Attachment attachment)
        {
            _context.Attachments.Add(attachment);
            await _context.SaveChangesAsync();
            return attachment;
        }

        public async Task DeleteAttachmentAsync(int id)
        {
            var attachment = await _context.Attachments.FindAsync(id);
            if (attachment != null)
            {
                _context.Attachments.Remove(attachment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAttachmentsByTransactionIdAsync(int transactionId)
        {
            var attachments = await _context.Attachments
                .Where(a => a.TransactionId == transactionId)
                .ToListAsync();

            if (attachments.Any())
            {
                _context.Attachments.RemoveRange(attachments);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> AttachmentExistsAsync(int id)
        {
            return await _context.Attachments.AnyAsync(a => a.Id == id);
        }
    }
}