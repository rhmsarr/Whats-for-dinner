using WhatsForDinner.Models;
namespace WhatsForDinner.ViewModels{
    public class GeneratedRecipe{
        public string RecipeName { get; set;} = string.Empty;
        public string CookTime { get; set; } = string.Empty;
        public string PrepTime { get; set; } = string.Empty;
        public string Servings { get; set; } = string.Empty;
        public List<RecipeIngredient> Ingredients { get; set; } 
        public List<RecipeStep> Steps { get; set; }
    }
}