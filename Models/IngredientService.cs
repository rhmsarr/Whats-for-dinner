using WhatsForDinner.Models;
using System.IO;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

public static class IngredientService{

    private static List<Ingredient> _ingredients = new();

    
    

    

    static IngredientService(){
        
       

    }
    public static List<Ingredient> ingredients{
            get{
               return _ingredients;
            }
        }

    

    public static List<Ingredient> GetByCategory(string _category){

        List<Ingredient> list = new List<Ingredient>();
        foreach(Ingredient ing in _ingredients){
            if(ing.IngredientCategory.Name == _category){
                list.Add(ing);
            }
        }

        return list;
    }
    public static void CreateIngredient(string _name, string _category){
        //_ingredients.Add(new Ingredient("_name","_category"));

    }


}