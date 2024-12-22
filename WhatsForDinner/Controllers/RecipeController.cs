using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhatsForDinner.Models;
using WhatsForDinner.Services;
using WhatsForDinner.ViewModels;

namespace WhatsForDinner.Controllers{
    public class RecipeController:Controller{

        private readonly DinnerDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly CohereService _cohereService;


        public RecipeController(DinnerDbContext context, UserManager<IdentityUser> userManager, CohereService cohereService){
            _context = context;
            _userManager = userManager;
            _cohereService = cohereService;
        }

        //method returning generated recipes
        public async Task<IActionResult> Generate(){
            
            string? userID = _userManager.GetUserId(User);
            if(userID == null){
                return RedirectToAction("Login", "Account",new{ returnUrl = "/Recipe/Generate"});
            }
            //saving the user's ingredients' names
            List<string> ingredients = _context.UsersIngredients.Where(i => i.UserId == userID)
                .Include(i => i.ingredient).Select(i => i.ingredient.Name).ToList();
            //calling the GenerateRecipeWithIngredients method, that creates an http call to Cohere's commad r+ model,
            //returning a list of recipes
            List<GeneratedRecipe> recipe = await _cohereService.GenerateRecipeWithIngredients(ingredients);

            
            
            

            return View(recipe);
        }
        [HttpPost]
        //method saving generated recipes
        public IActionResult SaveRecipe(GeneratedRecipe recipe){
            string? userID = _userManager.GetUserId(User);
            if(userID == null){
                return RedirectToAction("Login", "Account",new{ returnUrl = "/Recipe/SaveRecipe"});
            }
            //saving the main recipe data to the database
            _context.Recipes.Add(
                new Recipe {
                    RecipeName = recipe.RecipeName,
                    CookTime = recipe.CookTime,
                    PrepTime = recipe.PrepTime,
                    Servings = recipe.Servings,
                    UserId = userID

                });
            _context.SaveChanges();    
            //getting the recipeId of the newly created recipe
            int recipeID = _context.Recipes.Where(r => r.UserId == userID && r.RecipeName == recipe.RecipeName)
                            .First().RecipeId;
            //saving the recipe steps to the database
            foreach(RecipeStep step in recipe.Steps){
                step.RecipeId = recipeID;
                step.recipe = null;
                _context.RecipeSteps.Add(step);
            }
            //saving the ingredients to the database
            foreach(RecipeIngredient ing in recipe.Ingredients){
                ing.RecipeId = recipeID;
                ing.recipe = null;
                _context.RecipesIngredients.Add(ing);
            }
            _context.SaveChanges();

            return RedirectToAction("SavedRecipes");
        }

        //method returning the list of ingredients belonging to the user. 
        //Only the ingredient information is included, the rest is left to be returned by the MakeRecipe action.
        public IActionResult SavedRecipes(){
            string? userID = _userManager.GetUserId(User);
            if(userID == null){
                return RedirectToAction("Login", "Account", new{ returnUrl = "/Recipe/SavedRecipes"});
            }
            List<Recipe> recipes = _context.Recipes.Where(r => r.UserId == userID).Include(r => r.Ingredients).ToList();
            return View(recipes);
        }

            
        
        //method returning all the recipe information
        public IActionResult MakeRecipe(int id){
            string? userID = _userManager.GetUserId(User);
            //getting the recipe from it's ID and making sure it belongs to the logged in user
            Recipe recipe = _context.Recipes.Where(r => r.RecipeId == id).Include(r => r.Ingredients).Include(r => r.RecipeSteps).First();
            if(recipe == null || recipe.UserId != userID){
                return Redirect("SavedRecipes");
            }
            return View(recipe);
        }

        //method returning the recipe information for it to get updated
        public IActionResult UpdateRecipe(int id){
            //getting the user ID
            string? userID = _userManager.GetUserId(User);
            //getting the recipe from it's ID and making sure it belongs to the logged in user
            Recipe recipe = _context.Recipes.Where(r => r.RecipeId == id).Include(r => r.Ingredients).Include(r => r.RecipeSteps).First();
            if(recipe == null || recipe.UserId != userID){
                return Redirect("SavedRecipes");
            }
            return View(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRecipe(Recipe recipe){
            //updating the recipe steps
            foreach(RecipeStep step in recipe.RecipeSteps){
                step.recipe = null;
                _context.RecipeSteps.Update(step);
            }
            //updating the recipe ingredients 
            foreach(RecipeIngredient ing in recipe.Ingredients){
                ing.recipe = null;
                _context.RecipesIngredients.Update(ing);
            }
            //avoiding the creating of columns for ingredients and steps
            recipe.Ingredients = null;
            recipe.RecipeSteps = null;
            //updating the recipe
            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction("SavedRecipes");
        }

        public async Task<IActionResult> DeleteRecipe(int id){
            //finding the recipe from it's ID
            Recipe recipe = _context.Recipes.Where(r => r.RecipeId == id)
                    .Include(r => r.Ingredients).Include(r => r.RecipeSteps).First();
            //deleting all the recipe steps
            foreach(RecipeStep step in recipe.RecipeSteps){
                _context.RecipeSteps.Remove(step);
            }
            //deleting all the recipe ingredients 
            foreach(RecipeIngredient ing in recipe.Ingredients){
                _context.RecipesIngredients.Remove(ing);
            }
            
            //deleting the recipe
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction("SavedRecipes");

        }

        
    }
}