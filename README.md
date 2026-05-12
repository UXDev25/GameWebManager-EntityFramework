# VideoGameManager (EF Core Version)

VideoGameManager is a web application built with **C#** and **ASP.NET Core Razor Pages** designed to manage a video game collection. This version has been upgraded to use **Entity Framework Core** for persistent storage in **SQL Server**, supporting complex relationships and advanced data analytics.

## 🛠️ Project Structure

The project is organized to separate data access, models, and UI logic:

```text
VideoGameManagerEF/
├── Data/
│   └── GameStoreContext.cs       # EF Core Database Context
├── Migrations/                   # Database schema history
├── Models/                       # Data entities
│   ├── Enums/
│   ├── Game.cs                   # Game model with FK to Developer
│   └── Developer.cs              # Developer model (One-to-Many with Games)
├── Pages/
│   ├── Developers/               # Developer management (Index, Details)
│   ├── Games/                    # Games CRUD (with Developer selection)
│   ├── Stats/                    # LINQ Analytics and Advanced Research
│   └── Shared/
│       └── _Layout.cshtml        # Navigation including new sections
├── Services/                     # Business logic and IO operations
│   ├── FileManager.cs            # Legacy file handling for logs
│   └── ... (Exporters and Ranking)
├── appsettings.json              # Contains SQL Connection String
└── Program.cs                    # Service registration (DbContext)

```

## 🚀 Execution Instructions

### Prerequisites

* **.NET SDK** (Version 6.0 or higher)
* **SQL Server** (LocalDB or Express)
* **EF Core Tools**: `dotnet tool install --global dotnet-ef`

### Steps to Run

1. **Clone the repository**:
```bash
git clone <repository-url>
cd VideoGameManagerEF

```


2. **Update Connection String**:
Check `appsettings.json` and ensure the `DefaultConnection` points to your SQL instance.
3. **Database Migration**:
Apply the migrations to create the database and tables:
```bash
dotnet ef database update

```


4. **Run the application**:
```bash
dotnet run

```


5. **Access the web UI**:
Navigate to the local URL provided in the terminal (usually `https://localhost:7022`).

## 📊 New EF Core Features

### 1. Relational Data Model

The application implements a **One-to-Many** relationship between Developers and Games:

* **Developer**: A company that can have multiple games.
* **Game**: Includes a `DeveloperId` as a Foreign Key and a navigation property to access Developer data.
* **Referential Integrity**: The system prevents deleting a Developer if they have associated Games to maintain data consistency.

### 2. Advanced LINQ Analytics

A dedicated **Stats Dashboard** provides real-time insights:

* **Decade Grouping**: Categorizes the collection by release decades.
* **Performance Metrics**: Calculates the average score per developer.
* **Top 5 Rankings**: Displays the highest-rated games using `.OrderByDescending().Take(5)`.
* **Combined Research**: Advanced filtering by Title (case-insensitive), Genre, and Minimum Year.

## 📝 EF Core CLI Cheat Sheet

| Command | Description |
| --- | --- |
| `dotnet ef migrations add [Name]` | Creates a new migration file after model changes |
| `dotnet ef database update` | Applies pending migrations to the SQL database |
| `dotnet ef migrations list` | Lists all migrations and their status |
| `dotnet ef database drop` | Deletes the database for a clean reset |

## 📝 Commit Convention

This project follows the **Conventional Commits** specification:

* `feat:` New features (e.g., adding Developer stats).
* `fix:` Bug fixes (e.g., fixing Edit form validation).
* `db:` Database migrations and schema updates.
* `docs:` Documentation changes.
* `refactor:` Code improvements without changing functionality.

```

*Això farà que el desplegable es vegi bé (com a la Imatge 2) i no com una barra vertical buida.*
