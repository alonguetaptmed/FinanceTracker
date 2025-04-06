using FinanceTracker.Models;
using FinanceTracker.Repositories;
using FinanceTracker.ViewModels;
using Microsoft.AspNetCore.Http;

namespace FinanceTracker.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IFileService _fileService;

        public TransactionService(
            ITransactionRepository transactionRepository,
            ICategoryRepository categoryRepository,
            ITransactionCategoryRepository transactionCategoryRepository,
            IAttachmentRepository attachmentRepository,
            IFileService fileService)
        {
            _transactionRepository = transactionRepository;
            _categoryRepository = categoryRepository;
            _transactionCategoryRepository = transactionCategoryRepository;
            _attachmentRepository = attachmentRepository;
            _fileService = fileService;
        }

        public async Task<IEnumerable<TransactionViewModel>> GetAllTransactionsAsync()
        {
            var transactions = await _transactionRepository.GetAllTransactionsAsync();
            return transactions.Select(TransactionViewModel.FromTransaction);
        }

        public async Task<IEnumerable<TransactionViewModel>> GetRecentTransactionsAsync(int count)
        {
            var transactions = await _transactionRepository.GetRecentTransactionsAsync(count);
            return transactions.Select(TransactionViewModel.FromTransaction);
        }

        public async Task<IEnumerable<TransactionViewModel>> GetTransactionsByAccountAsync(int accountId)
        {
            var transactions = await _transactionRepository.GetTransactionsByAccountAsync(accountId);
            return transactions.Select(TransactionViewModel.FromTransaction);
        }

        public async Task<IEnumerable<TransactionViewModel>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var transactions = await _transactionRepository.GetTransactionsByDateRangeAsync(startDate, endDate);
            return transactions.Select(TransactionViewModel.FromTransaction);
        }

        public async Task<IEnumerable<TransactionViewModel>> GetTransactionsByCategoryAsync(int categoryId)
        {
            var transactions = await _transactionRepository.GetTransactionsByCategoryAsync(categoryId);
            return transactions.Select(TransactionViewModel.FromTransaction);
        }

        public async Task<IEnumerable<TransactionViewModel>> GetFilteredTransactionsAsync(int? accountId, DateTime? startDate, DateTime? endDate, TransactionType? type)
        {
            var transactions = await _transactionRepository.GetFilteredTransactionsAsync(accountId, startDate, endDate, type);
            return transactions.Select(TransactionViewModel.FromTransaction);
        }

        public async Task<TransactionViewModel?> GetTransactionByIdAsync(int id)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(id);
            return transaction != null ? TransactionViewModel.FromTransaction(transaction) : null;
        }

        public async Task<TransactionViewModel> CreateTransactionAsync(TransactionViewModel viewModel, IFormFileCollection files)
        {
            // Conversion du ViewModel en modèle
            var transaction = viewModel.ToTransaction();
            
            // Ajout de la transaction
            await _transactionRepository.AddTransactionAsync(transaction);
            
            // Gestion des catégories
            decimal remainingAmount = transaction.Amount;
            var selectedCategories = await _categoryRepository.GetCategoriesByIdsAsync(viewModel.SelectedCategoryIds);
            
            // Si aucun montant spécifique n'est fourni, répartir équitablement
            if (viewModel.Categories == null || !viewModel.Categories.Any())
            {
                int categoryCount = viewModel.SelectedCategoryIds.Count;
                decimal amountPerCategory = transaction.Amount / categoryCount;
                
                foreach (var category in selectedCategories)
                {
                    var transactionCategory = new TransactionCategory
                    {
                        TransactionId = transaction.Id,
                        CategoryId = category.Id,
                        Amount = Math.Round(amountPerCategory, 2)
                    };
                    
                    await _transactionCategoryRepository.AddTransactionCategoryAsync(transactionCategory);
                }
            }
            else
            {
                // Utiliser les montants fournis pour chaque catégorie
                foreach (var categoryViewModel in viewModel.Categories)
                {
                    if (viewModel.SelectedCategoryIds.Contains(categoryViewModel.CategoryId))
                    {
                        var transactionCategory = new TransactionCategory
                        {
                            TransactionId = transaction.Id,
                            CategoryId = categoryViewModel.CategoryId,
                            Amount = categoryViewModel.Amount
                        };
                        
                        await _transactionCategoryRepository.AddTransactionCategoryAsync(transactionCategory);
                    }
                }
            }
            
            // Gestion des pièces jointes
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var attachment = await _fileService.SaveFileAsync(file, transaction.Id);
                        await _attachmentRepository.AddAttachmentAsync(attachment);
                    }
                }
            }
            
            // Récupération de la transaction complète pour le retour
            var createdTransaction = await _transactionRepository.GetTransactionByIdAsync(transaction.Id);
            return TransactionViewModel.FromTransaction(createdTransaction!);
        }

        public async Task<TransactionViewModel> UpdateTransactionAsync(TransactionViewModel viewModel, IFormFileCollection files)
        {
            // Conversion du ViewModel en modèle
            var transaction = viewModel.ToTransaction();
            
            // Mise à jour de la transaction
            await _transactionRepository.UpdateTransactionAsync(transaction);
            
            // Suppression des catégories existantes
            await _transactionCategoryRepository.DeleteTransactionCategoriesByTransactionIdAsync(transaction.Id);
            
            // Ajout des nouvelles catégories
            decimal remainingAmount = transaction.Amount;
            var selectedCategories = await _categoryRepository.GetCategoriesByIdsAsync(viewModel.SelectedCategoryIds);
            
            // Si aucun montant spécifique n'est fourni, répartir équitablement
            if (viewModel.Categories == null || !viewModel.Categories.Any())
            {
                int categoryCount = viewModel.SelectedCategoryIds.Count;
                decimal amountPerCategory = transaction.Amount / categoryCount;
                
                foreach (var category in selectedCategories)
                {
                    var transactionCategory = new TransactionCategory
                    {
                        TransactionId = transaction.Id,
                        CategoryId = category.Id,
                        Amount = Math.Round(amountPerCategory, 2)
                    };
                    
                    await _transactionCategoryRepository.AddTransactionCategoryAsync(transactionCategory);
                }
            }
            else
            {
                // Utiliser les montants fournis pour chaque catégorie
                foreach (var categoryViewModel in viewModel.Categories)
                {
                    if (viewModel.SelectedCategoryIds.Contains(categoryViewModel.CategoryId))
                    {
                        var transactionCategory = new TransactionCategory
                        {
                            TransactionId = transaction.Id,
                            CategoryId = categoryViewModel.CategoryId,
                            Amount = categoryViewModel.Amount
                        };
                        
                        await _transactionCategoryRepository.AddTransactionCategoryAsync(transactionCategory);
                    }
                }
            }
            
            // Gestion des nouveaux fichiers
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var attachment = await _fileService.SaveFileAsync(file, transaction.Id);
                        await _attachmentRepository.AddAttachmentAsync(attachment);
                    }
                }
            }
            
            // Récupération de la transaction complète pour le retour
            var updatedTransaction = await _transactionRepository.GetTransactionByIdAsync(transaction.Id);
            return TransactionViewModel.FromTransaction(updatedTransaction!);
        }

        public async Task DeleteTransactionAsync(int id)
        {
            // Suppression des fichiers associés
            var attachments = await _attachmentRepository.GetAttachmentsByTransactionIdAsync(id);
            foreach (var attachment in attachments)
            {
                await _fileService.DeleteFileAsync(attachment.FilePath);
            }
            
            // Suppression des pièces jointes en BDD
            await _attachmentRepository.DeleteAttachmentsByTransactionIdAsync(id);
            
            // Suppression des catégories associées
            await _transactionCategoryRepository.DeleteTransactionCategoriesByTransactionIdAsync(id);
            
            // Suppression de la transaction
            await _transactionRepository.DeleteTransactionAsync(id);
        }

        public async Task<bool> TransactionExistsAsync(int id)
        {
            return await _transactionRepository.TransactionExistsAsync(id);
        }

        public async Task<decimal> GetTotalExpensesAsync(DateTime startDate, DateTime endDate, int? accountId = null)
        {
            var transactions = await _transactionRepository.GetFilteredTransactionsAsync(
                accountId, startDate, endDate, TransactionType.Debit);
            
            return transactions.Sum(t => t.Amount);
        }

        public async Task<decimal> GetTotalIncomeAsync(DateTime startDate, DateTime endDate, int? accountId = null)
        {
            var transactions = await _transactionRepository.GetFilteredTransactionsAsync(
                accountId, startDate, endDate, TransactionType.Credit);
            
            return transactions.Sum(t => t.Amount);
        }
    }
}