using Microsoft.AspNetCore.Mvc;

namespace WhatsForDinner.Models{

    public class Recipe{
        public int RecipeId { get; set;}
        public string UserId { get; set;} = string.Empty;

        public string RecipeName { get; set; } = string.Empty;
        public string CookTime { get; set; } = string.Empty;
        public string PrepTime { get; set; } = string.Empty;
        public string Servings { get; set; } = string.Empty;
        
        public List<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();
        public List<RecipeStep> RecipeSteps { get; set;} = new List<RecipeStep>();

    }
    
}