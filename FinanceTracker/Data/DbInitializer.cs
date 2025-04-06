using FinanceTracker.Models;

namespace FinanceTracker.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Vérifiez s'il y a déjà des catégories
            if (context.Categories.Any())
            {
                return; // DB déjà ensemencée
            }

            // Créer des catégories par défaut
            var categories = new Category[]
            {
                new Category { Name = "Alimentation", Description = "Courses, restaurants, etc.", Color = "#2ecc71" },
                new Category { Name = "Logement", Description = "Loyer, électricité, eau, etc.", Color = "#3498db" },
                new Category { Name = "Transport", Description = "Carburant, tickets, entretien du véhicule, etc.", Color = "#9b59b6" },
                new Category { Name = "Loisirs", Description = "Sports, cinéma, sorties, etc.", Color = "#e67e22" },
                new Category { Name = "Santé", Description = "Médecin, pharmacie, etc.", Color = "#e74c3c" },
                new Category { Name = "Éducation", Description = "Cours, livres, formations, etc.", Color = "#1abc9c" },
                new Category { Name = "Revenus", Description = "Salaires, primes, etc.", Color = "#f1c40f" },
                new Category { Name = "Épargne", Description = "Épargne et investissements", Color = "#34495e" },
                new Category { Name = "Divers", Description = "Dépenses diverses", Color = "#95a5a6" }
            };

            foreach (var category in categories)
            {
                context.Categories.Add(category);
            }
            context.SaveChanges();

            // Créer un compte par défaut
            var account = new Account
            {
                Name = "Compte courant",
                InitialBalance = 1000,
                CreatedAt = DateTime.Now
            };
            context.Accounts.Add(account);
            context.SaveChanges();

            // Ajouter quelques transactions d'exemple
            var transactions = new Transaction[]
            {
                new Transaction {
                    Type = TransactionType.Credit,
                    Amount = 2500,
                    Date = DateTime.Now.AddDays(-15),
                    Notes = "Salaire",
                    AccountId = account.Id,
                    CreatedAt = DateTime.Now.AddDays(-15)
                },
                new Transaction {
                    Type = TransactionType.Debit,
                    Amount = 650,
                    Date = DateTime.Now.AddDays(-10),
                    Notes = "Loyer",
                    AccountId = account.Id,
                    CreatedAt = DateTime.Now.AddDays(-10)
                },
                new Transaction {
                    Type = TransactionType.Debit,
                    Amount = 120,
                    Date = DateTime.Now.AddDays(-7),
                    Notes = "Courses",
                    AccountId = account.Id,
                    CreatedAt = DateTime.Now.AddDays(-7)
                },
                new Transaction {
                    Type = TransactionType.Debit,
                    Amount = 35,
                    Date = DateTime.Now.AddDays(-3),
                    Notes = "Restaurant",
                    AccountId = account.Id,
                    CreatedAt = DateTime.Now.AddDays(-3)
                }
            };

            foreach (var transaction in transactions)
            {
                context.Transactions.Add(transaction);
            }
            context.SaveChanges();

            // Associer les catégories aux transactions
            var transactionCategories = new TransactionCategory[]
            {
                new TransactionCategory {
                    TransactionId = transactions[0].Id,
                    CategoryId = categories[6].Id, // Revenus
                    Amount = 2500
                },
                new TransactionCategory {
                    TransactionId = transactions[1].Id,
                    CategoryId = categories[1].Id, // Logement
                    Amount = 650
                },
                new TransactionCategory {
                    TransactionId = transactions[2].Id,
                    CategoryId = categories[0].Id, // Alimentation
                    Amount = 120
                },
                new TransactionCategory {
                    TransactionId = transactions[3].Id,
                    CategoryId = categories[0].Id, // Alimentation
                    Amount = 35
                }
            };

            foreach (var tc in transactionCategories)
            {
                context.TransactionCategories.Add(tc);
            }
            context.SaveChanges();
        }
    }
}