using System.ComponentModel.DataAnnotations;

namespace WhatsForDinner.Models{
    public class Ingredient{

        [Key]
        public int IngredientId { get; set; }
        public string Name{ get; set; }
        public Category Category{ get; set; }

        public Ingredient(string _name, string _category){
            Name=_name;
            Category=new Category();
            Category.Name=_category;
        }
    }
}