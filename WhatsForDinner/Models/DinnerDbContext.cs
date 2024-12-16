using Microsoft.EntityFrameworkCore;

namespace WhatsForDinner.Models{
    public class DinnerDbContext : DbContext{
        public DinnerDbContext(DbContextOptions<DinnerDbContext> options) : base(options) { }

        public DbSet<Ingredient> Ingredients => Set<Ingredient>(); //creating the Ingredients table

        public DbSet<IngredientCategory> Categories => Set<IngredientCategory>(); //creating the Categories table

        public DbSet<Recipe> Recipes => Set<Recipe>();
        public DbSet<RecipeIngredient> RecipesIngredients => Set<RecipeIngredient>();
        public DbSet<RecipeStep> RecipeSteps=> Set<RecipeStep>();
        public DbSet<IngredientUser> UsersIngredients => Set<IngredientUser>();
    }
}