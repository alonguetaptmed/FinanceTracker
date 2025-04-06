# Finance Tracker

Application web MVC en C# (.NET 8) pour la gestion de comptes bancaires et le suivi des opérations financières.

## Fonctionnalités

1. **Gestion des Comptes bancaires**
   - Création / Édition / Suppression de comptes
   - Chaque compte a un nom et un solde initial (optionnel)

2. **Enregistrement des opérations (dépense ou gain)**
   - Formulaire pour créer une opération avec:
     - Type : Crédit ou Débit
     - Montant
     - Date
     - Compte associé (obligatoire)
     - Une ou plusieurs catégories
     - Répartition des montants entre catégories
     - Notes (optionnel)
     - Pièces jointes (photos/fichiers)

3. **Gestion des Catégories**
   - Création / Édition / Suppression de catégories
   - Répartition des dépenses par catégorie

4. **Tableau de bord / Statistiques**
   - Dépenses par catégorie (graphiques)
   - Filtrage par date, compte, type (Crédit/Débit)
   - Affichage des soldes par compte

## Prérequis

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/fr/vs/) (ou Visual Studio Code)
- SQL Server LocalDB (inclus avec Visual Studio)

## Installation et démarrage

1. **Cloner le dépôt**
   ```
   git clone https://github.com/alonguetaptmed/FinanceTracker.git
   cd FinanceTracker
   ```

2. **Restaurer les packages NuGet**
   ```
   dotnet restore
   ```

3. **Appliquer les migrations pour créer la base de données**
   ```
   cd FinanceTracker
   dotnet ef database update
   ```

4. **Lancer l'application**
   ```
   dotnet run
   ```
   Ou ouvrir la solution dans Visual Studio et exécuter le projet.

5. **Accéder à l'application**
   Ouvrir un navigateur et accéder à `https://localhost:7210` ou `http://localhost:5210`

## Technologies utilisées

- ASP.NET Core MVC 8.0
- Entity Framework Core 8.0 (Code First)
- Bootstrap 5
- SQL Server LocalDB
- Chart.js (pour les graphiques)

## Structure de l'application

- Architecture MVC
- Pattern Repository et Service
- Migration Entity Framework
- Base de données SQL Server
