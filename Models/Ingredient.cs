using System.ComponentModel.DataAnnotations;

namespace WhatsForDinner.Models{
    public class Ingredient{

        [Key]
        public int Id { get; set; }
        public string name{ get; set; }
        public string category{ get; set; }

        public Ingredient(string _name, string _category){
            name=_name;
            category=_category;
        }
    }
}