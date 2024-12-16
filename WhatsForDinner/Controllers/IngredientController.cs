using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhatsForDinner.Models;

namespace WhatsForDinner.Controllers{

    public class IngredientController : Controller{

        private readonly DinnerDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public IngredientController(DinnerDbContext context, UserManager<IdentityUser> userManager){
            _context = context;
            _userManager = userManager;
        }


        [Route("MyAccount/MyPantry")]
        public IActionResult Index(){
            var ingredients = _context.Ingredients.Include(i => i.IngredientCategory).AsQueryable();
            List<Ingredient> ingredientsList = ingredients.OrderBy(i => i.IngredientCategory.Name).ToList();
            return View(ingredientsList);
        }

        [HttpPost]
        public IActionResult SaveIngredients(List<string> SelectedIngredients){
            
            string? userID = _userManager.GetUserId(User);

            if(userID == null){
                return Redirect("Account/Login");
            }

            List<string> savedIngNames = _context.UsersIngredients.Where(i => i.UserId == userID).Include(i=>i.ingredient).Select(i => i.ingredient.Name).ToList();
            List<string> ingredientsToAdd = SelectedIngredients.Except(savedIngNames).ToList();
            List<string> ingredientsToRemove = savedIngNames.Except(SelectedIngredients).ToList();

            
                foreach(string ing in ingredientsToAdd){
                    IngredientUser ingredient = new IngredientUser{UserId = userID, IngredientId = _context.Ingredients.Where(i => i.Name == ing).FirstOrDefault().IngredientId};
                    _context.UsersIngredients.Add(ingredient);
                }
                foreach(string ing in ingredientsToRemove){
                   var ingredient = _context.UsersIngredients.Where(i => i.ingredient.Name == ing && i.UserId == userID).FirstOrDefault();
                   _context.UsersIngredients.Remove(ingredient);

                
                }
            
            _context.SaveChanges();


            return RedirectToAction("Index");
        }

    }
}