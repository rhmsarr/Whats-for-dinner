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

            GeneratedRecipe recipe = await _cohereService.GenerateRecipeWithIngredients(ingredients);

            /*foreach(RecipeIngredient ing in recipe.Ingredients){
                var ingredient = _context.Ingredients.First(i => i.IngredientId == ing.IngredientId);
                ing.ing = new Ingredient(){
                    Name = ingredient.Name
                };

            }*/

            //var ingredientIds = recipe.Ingredients.Select(i => i.IngredientId).ToList();
            //var ingredients = _context.Ingredients.Where(i => ingredientIds.Contains(i.IngredientId)).ToList();

            
            

            return View(recipe);
        }

        public IActionResult RecipesList(){
            return View();
        }
        
    }
}