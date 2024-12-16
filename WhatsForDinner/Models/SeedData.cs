using Microsoft.EntityFrameworkCore;

namespace WhatsForDinner.Models{

    public static class SeedData{ 
        
        public static void EnsurePopulated(IApplicationBuilder app){
            DinnerDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<DinnerDbContext>();

            if(context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            
            //populating the database with data from the ingredients.csv file.

            if(!context.Ingredients.Any()){
                 //saving the ingredients and categories not present in the database to populate later on
                List<IngredientCategory> categoriesToAdd = new List<IngredientCategory>();
                List<Ingredient> ingredientsToAdd = new List<Ingredient>();
                var filePath = "wwwroot/Data/ingredients.csv";
                
                if(System.IO.File.Exists(filePath)){
                //opening the file
                    using (var reader = new StreamReader(filePath)){
                       
                        //reading each line of the file

                        while(!reader.EndOfStream){
                            var line = reader.ReadLine();
                            if(line.Contains(','))//separating the line into two parts, category and ingredient name
                            {   
                                var values = line.Split(',');
                                var ingredientName = values[0];
                                var categoryName = values[1];

                                //checking if the category already exists in the database
                                IngredientCategory category = context.Categories.FirstOrDefault(c => c.Name == categoryName);

                                if(category == null){
                                    if(!categoriesToAdd.Any(c => c.Name == categoryName)){
                                        category = new IngredientCategory {Name = categoryName};
                                        categoriesToAdd.Add(category);
                                    }
                                }

                                
                                //saving the ingredient and its category to add to the databse later on
                                Ingredient ingredient = context.Ingredients.FirstOrDefault(i => i.Name == ingredientName);
                                if(ingredient == null){
                                    if(!ingredientsToAdd.Any(i => i.Name == ingredientName)){
                                        
                                    }
                                    ingredient = new Ingredient {
                                        Name = ingredientName, 
                                        IngredientCategory = new IngredientCategory{Name = categoryName}
                                        };
                                    ingredientsToAdd.Add(ingredient);
                                }
                                
                            }
                            else
                                continue;
                        }
                   }
                }
                //saving the retrieved categories in the database
                if(categoriesToAdd.Count > 0){
                    foreach(var category in categoriesToAdd){
                        context.Categories.Add(category);
                    }
                    context.SaveChanges();
                }
                //saving the retrieved ingredients in the database
                if(ingredientsToAdd.Count > 0){
                    foreach(var ingredient in ingredientsToAdd){
                        var ingCategoryId = context.Categories
                            .First(c => c.Name == ingredient.IngredientCategory.Name)
                            .IngredientCategoryId;
                            ingredient.IngredientCategoryId = ingCategoryId;

                        ingredient.IngredientCategory = null; //avoiding the creation of a new category record in the db
                        context.Ingredients.Add(ingredient);
                    }
                    context.SaveChanges();
                }
         
            }
            

            context.SaveChanges();

        }
    }
}