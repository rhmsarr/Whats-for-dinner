using System.ComponentModel.DataAnnotations;

namespace WhatsForDinner.Models{
    public class Ingredient{

        public int IngredientId { get; set; }
        public string Name{ get; set; } = String.Empty;

        
        public long IngredientCategoryId { get; set; }
        public IngredientCategory IngredientCategory { get; set; }
        
        /*public Ingredient(string _name, string _category){
            Name=_name;
            Category=new Category();
            Category.Name=_category;
        }*/
    }
}