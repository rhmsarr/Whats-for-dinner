using Microsoft.EntityFrameworkCore;

namespace WhatsForDinner.Models{

    public static class SeedData{ 
        
        public static void EnsurePopulated(IApplicationBuilder app){
            DinnerDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<DinnerDbContext>();

            if(context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }   
            if(!context.Ingredients.Any()){
                var filePath = "wwwroot/Data/ingredients.csv";
                if(System.IO.File.Exists(filePath)){

                using (var reader = new StreamReader(filePath)){
                
                while(!reader.EndOfStream){
                    var line = reader.ReadLine();
                    if(line.Contains(','))
                    {   
                        var values = line.Split(',');
                        if(!context.Categories.Any(cat => cat.Name == values[1])){
                            IngredientCategory newCat = new IngredientCategory{
                                Name = values[1]
                            };
                            context.Categories.Add(newCat);
                        }
                        long catID = context.Categories.FirstOrDefault(cat => cat.Name == values[1]).IngredientCategoryId;
                        context.Ingredients.Add(
                            new Ingredient{
                                Name = values[0],
                                IngredientCategoryId = catID
                            });
                    
                        
                    }
                    else
                        continue;
               }
            }
        }
            }         

        }
    }
}