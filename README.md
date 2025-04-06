# Finance Tracker

Application web MVC en C# (.NET 8) pour la gestion de comptes bancaires et le suivi des opérations financières. Cette application permet de gérer vos finances personnelles en enregistrant vos dépenses et revenus, en les catégorisant et en visualisant votre situation financière à l'aide de graphiques.

## Fonctionnalités

1. **Gestion des Comptes bancaires**
   - Création / Édition / Suppression de comptes
   - Chaque compte a un nom et un solde initial (optionnel)
   - Visualisation du solde actuel calculé à partir des transactions

2. **Enregistrement des opérations (dépenses ou gains)**
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
   - Attribution de couleurs personnalisées
   - Répartition des dépenses par catégorie

4. **Tableau de bord / Statistiques**
   - Dépenses par catégorie (graphiques en camembert)
   - Évolution du solde (graphiques linéaires)
   - Filtrage par date, compte, type (Crédit/Débit)
   - Affichage des soldes par compte
   - Affichage des transactions récentes

## Captures d'écran

*L'application comprend une interface utilisateur moderne et responsive:*

- **Tableau de bord**: Visualisation des dépenses et revenus avec filtres
- **Comptes**: Gestion des comptes bancaires
- **Transactions**: Liste et formulaire détaillé pour les opérations
- **Catégories**: Organisation des dépenses par catégorie avec code couleur

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
   dotnet ef database update
   ```
   Ou depuis Visual Studio, ouvrez la Console du Gestionnaire de Package et exécutez:
   ```
   Update-Database
   ```

4. **Lancer l'application**
   ```
   dotnet run --project FinanceTracker
   ```
   Ou ouvrir la solution dans Visual Studio et exécuter le projet.

5. **Accéder à l'application**
   Ouvrir un navigateur et accéder à `https://localhost:7210` ou `http://localhost:5210`

## Technologies utilisées

- ASP.NET Core MVC 8.0
- Entity Framework Core 8.0 (Code First)
- SQL Server LocalDB
- Bootstrap 5 pour l'interface utilisateur
- Chart.js pour les graphiques
- Architecture en couches (Repositories, Services, Controllers)

## Structure de l'application

- **Models**: Définition des entités de base de données
- **ViewModels**: Objets de transfert pour les vues
- **Controllers**: Logique de traitement des requêtes HTTP
- **Views**: Interface utilisateur
- **Services**: Logique métier
- **Repositories**: Accès aux données
- **Data**: Contexte EF Core et migrations
- **wwwroot**: Ressources statiques (CSS, JS, images)

## Fonctionnalités avancées

- **Upload de fichiers**: Possibilité d'attacher des fichiers aux transactions
- **Répartition des montants**: Allocation de montants entre différentes catégories
- **Graphiques dynamiques**: Visualisation des données financières
- **Responsive Design**: Interface adaptée à tous les appareils

## Contribution

Ce projet a été créé comme exemple d'application MVC en C# (.NET 8). Les contributions sont les bienvenues via pull requests.

## Licence

Ce projet est sous licence MIT. Voir le fichier LICENSE pour plus de détails.
