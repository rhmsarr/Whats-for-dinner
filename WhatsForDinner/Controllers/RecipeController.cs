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

        public async Task<IActionResult> Generate(){

            string? userID = _userManager.GetUserId(User);
            if(userID == null){
                return Redirect("Account/Login");
            }

            List<string> ingredients = _context.UsersIngredients.Where(i => i.UserId == userID)
                .Include(i => i.ingredient).Select(i => i.ingredient.Name).ToList();

            List<GeneratedRecipe> recipe = await _cohereService.GenerateRecipeWithIngredients(ingredients);

            
            
            

            return View(recipe);
        }
        [HttpPost]
        public IActionResult SaveRecipe(GeneratedRecipe recipe){
            string? userID = _userManager.GetUserId(User);
            if(userID == null){
                return Redirect("Account/Login");
            }
            _context.Recipes.Add(
                new Recipe {
                    RecipeName = recipe.RecipeName,
                    CookTime = recipe.CookTime,
                    PrepTime = recipe.PrepTime,
                    Servings = recipe.Servings,
                    UserId = userID

                });
            _context.SaveChanges();    

            int recipeID = _context.Recipes.Where(r => r.UserId == userID && r.RecipeName == recipe.RecipeName)
                            .First().RecipeId;
            foreach(RecipeStep step in recipe.Steps){
                step.RecipeId = recipeID;
                _context.RecipeSteps.Add(step);
            }
            foreach(RecipeIngredient ing in recipe.Ingredients){
                ing.RecipeId = recipeID;
                _context.RecipesIngredients.Add(ing);
            }
            _context.SaveChanges();

            return RedirectToAction("SavedRecipes");
        }



            
        

        public IActionResult RecipesList(){
            return View();
        }
        
    }
}