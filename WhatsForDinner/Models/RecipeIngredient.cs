namespace WhatsForDinner.Models{
    public class RecipeIngredient{

        public int RecipeIngredientId { get; set; }
        public string IngredientName { get; set;} = string.Empty;
        public int RecipeId { get; set;}
        public string quantity { get; set;} = string.Empty;

        //public Ingredient ing {get; set;}
        public Recipe recipe { get; set;}
    }
}