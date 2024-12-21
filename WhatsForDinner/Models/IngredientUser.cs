using Microsoft.EntityFrameworkCore;

namespace WhatsForDinner.Models{

    public class IngredientUser{

        public int IngredientUserId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int IngredientId { get; set; }
        public Ingredient ingredient{ get; set; } = new Ingredient();
        
    }
    
}