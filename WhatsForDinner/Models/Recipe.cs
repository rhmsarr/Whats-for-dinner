using Microsoft.AspNetCore.Mvc;

namespace WhatsForDinner.Models{

    public class Recipe{
        public int RecipeId { get; set;}
        public string RecipeName { get; set; } = string.Empty;
        public string CookTime { get; set; } = string.Empty;
        public string PrepTime { get; set; } = string.Empty;
        public int Servings { get; set; }
        
    }
    
}