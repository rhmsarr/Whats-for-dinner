namespace WhatsForDinner.Models{
    public class RecipeIngredient{

        public int RecipeIngredientId { get; set; }
        public int IngredientId { get; set;}
        public int RecipeId { get; set;}
        public string quantity { get; set;} = string.Empty;
    }
}