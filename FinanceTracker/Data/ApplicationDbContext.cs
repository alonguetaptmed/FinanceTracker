using FinanceTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration pour Account
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Transactions)
                .WithOne(t => t.Account)
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration pour Transaction
            modelBuilder.Entity<Transaction>()
                .HasMany(t => t.TransactionCategories)
                .WithOne(tc => tc.Transaction)
                .HasForeignKey(tc => tc.TransactionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>()
                .HasMany(t => t.Attachments)
                .WithOne(a => a.Transaction)
                .HasForeignKey(a => a.TransactionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration pour Category
            modelBuilder.Entity<Category>()
                .HasMany(c => c.TransactionCategories)
                .WithOne(tc => tc.Category)
                .HasForeignKey(tc => tc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration pour TransactionCategory
            modelBuilder.Entity<TransactionCategory>()
                .HasKey(tc => tc.Id);

            modelBuilder.Entity<TransactionCategory>()
                .HasIndex(tc => new { tc.TransactionId, tc.CategoryId })
                .IsUnique();
        }
    }
}