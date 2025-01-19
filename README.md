# What's For Dinner

A web application built with ASP.NET Core that leverages AI to generate unique recipes based on the ingredients selected by the user. The recipes are generated through API calls and can be saved to a database for future reference.

## Features

- **Ingredient-Based Recipe Generation**: Select ingredients, and the AI generates creative recipes tailored to your input.
- **Database Integration**: Save recipes to your account for future access.
- **User-Friendly Interface**: Intuitive design to make recipe discovery fun and easy.

## Getting Started

Follow these instructions to get a local copy of the project up and running.

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Cohere API Key](https://cohere.ai/) (required for recipe generation)
- A database (e.g., SQL Server or another supported by ASP.NET Core EF Core)

### Installation

1. **Clone the repository**:

   ```bash
   git clone https://github.com/yourusername/ai-recipe-generator.git
   cd ai-recipe-generator
   ```

2. **Set up the API Key**:

   - Open the `appsettings.json` file in the root directory.
   - Add your Cohere API key under the `ApiKey` field:
     ```json
     "CohereSettings": {
         "ApiKey": "Your_API_Key"  }
     ```

3. **Restore dependencies**:

   ```bash
   dotnet restore
   ```

4. **Set up the database**:

   - Update the connection string in `appsettings.json` under `ConnectionStrings`:
     ```json
     {
       "ConnectionStrings": {
         "database": "Your_main_database",
         "identityDatabase": "Your_user_info_database"
       }
     }
     ```
   - Apply migrations to set up the database schema:
     ```bash
     dotnet ef database update
     ```

5. **Run the application**:

   ```bash
   dotnet run
   ```

   The application will be available at `http://localhost:44154`.

### Usage

1. Login.
2. Navigate to the Ingredients page using the navbar.
3. Select the ingredients you have on hand.
4. Click the "Generate Recipe" button on the home page to receive AI-powered recipe suggestions.
5. Optionally, save recipes to your account for future use.

## Built With

- **ASP.NET Core**: Backend framework
- **Entity Framework Core**: ORM for database operations
- **Cohere API**: AI-powered recipe generation
- **Bootstrap**: Frontend styling

## Contributing

Contributions are welcome! To contribute:

1. Fork the repository.
2. Create a new branch for your feature/bugfix:
   ```bash
   git checkout -b feature-name
   ```
3. Commit your changes:
   ```bash
   git commit -m "Add feature-name"
   ```
4. Push to your branch:
   ```bash
   git push origin feature-name
   ```
5. Open a pull request.

##

---

Enjoy using the AI-Powered Recipe Generator! 🍳


