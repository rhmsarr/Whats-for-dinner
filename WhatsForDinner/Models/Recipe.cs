using Microsoft.AspNetCore.Mvc;

namespace WhatsForDinner.Models{

    public class Recipe{
        public int RecipeId { get; set;}
        public string UserId { get; set;} = string.Empty;

        public string RecipeName { get; set; } = string.Empty;
        public string CookTime { get; set; } = string.Empty;
        public string PrepTime { get; set; } = string.Empty;
        public string Servings { get; set; } = string.Empty;
        
    }
    
}