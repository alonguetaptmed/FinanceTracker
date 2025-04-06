using FinanceTracker.Models;
using FinanceTracker.Services;
using FinanceTracker.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanceTracker.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;
        private readonly ICategoryService _categoryService;

        public TransactionController(
            ITransactionService transactionService,
            IAccountService accountService,
            ICategoryService categoryService)
        {
            _transactionService = transactionService;
            _accountService = accountService;
            _categoryService = categoryService;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            return View(transactions);
        }

        // GET: Transaction/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transaction/Create
        public async Task<IActionResult> Create()
        {
            // Préparer les listes déroulantes pour les comptes et catégories
            await PopulateDropdownLists();
            
            return View(new TransactionViewModel
            {
                Date = DateTime.Today,
                Type = TransactionType.Debit // Par défaut, on choisit une dépense
            });
        }

        // POST: Transaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _transactionService.CreateTransactionAsync(viewModel, Request.Form.Files);
                return RedirectToAction(nameof(Index));
            }
            
            // En cas d'erreur, recharger les listes déroulantes
            await PopulateDropdownLists();
            return View(viewModel);
        }

        // GET: Transaction/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            
            // Charger les listes déroulantes
            await PopulateDropdownLists();
            
            return View(transaction);
        }

        // POST: Transaction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TransactionViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _transactionService.UpdateTransactionAsync(viewModel, Request.Form.Files);
                }
                catch
                {
                    if (!await _transactionService.TransactionExistsAsync(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            // En cas d'erreur, recharger les listes déroulantes
            await PopulateDropdownLists();
            return View(viewModel);
        }

        // GET: Transaction/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _transactionService.DeleteTransactionAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // DELETE: Transaction/DeleteAttachment/5
        [HttpDelete]
        public async Task<IActionResult> DeleteAttachment(int id)
        {
            // Implémentation à faire pour supprimer une pièce jointe via AJAX
            return Json(new { success = true });
        }

        // GET: Transaction/GetCategories
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Json(categories);
        }

        // GET: Transaction/GetTransactionsByCategory/5
        [HttpGet]
        public async Task<IActionResult> GetTransactionsByCategory(int id)
        {
            var transactions = await _transactionService.GetTransactionsByCategoryAsync(id);
            return Json(transactions);
        }

        // Méthode privée pour préparer les listes déroulantes
        private async Task PopulateDropdownLists()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            ViewBag.Accounts = new SelectList(accounts, "Id", "Name");

            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = categories.ToList();

            ViewBag.TransactionTypes = new SelectList(Enum.GetValues(typeof(TransactionType))
                .Cast<TransactionType>()
                .Select(t => new { Id = (int)t, Name = t.ToString() }), "Id", "Name");
        }
    }
}