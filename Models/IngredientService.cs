using WhatsForDinner.Models;
using System.IO;

public static class IngredientService{

    private static List<Ingredient> _ingredients = new();

    
    

    

    static IngredientService(){
        
        /*var filePath = "wwwroot/Data/ingredients.csv";
        if(System.IO.File.Exists(filePath)){
             using (var reader = new StreamReader(filePath)){
                while(!reader.EndOfStream){
                    var line = reader.ReadLine();
                    if(line.Contains(','))
                    {    var values = line.Split(',');
                        _ingredients.Add(new Ingredient(values[0], values[1]));
                    }
                    else
                        continue;
               }
            }
        }
        else{
            Console.WriteLine(filePath);
        }*/
       
       

    }
    public static List<Ingredient> ingredients{
            get{
               return _ingredients;
            }
        }

    

    public static List<Ingredient> GetByCategory(string _category){

        List<Ingredient> list = new List<Ingredient>();
        foreach(Ingredient ing in _ingredients){
            if(ing.Category.Name == _category){
                list.Add(ing);
            }
        }

        return list;
    }
    public static void CreateIngrdient(string _name, string _category){
        //_ingredients.Add(new Ingredient("_name","_category"));

    }


}