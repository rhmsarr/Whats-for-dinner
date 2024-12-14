using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhatsForDinner.Models;

namespace WhatsForDinner.Controllers{

    public class IngredientController : Controller{

        private readonly DinnerDbContext _context;

        public IngredientController(DinnerDbContext context){
            _context = context;
        }


        [Route("MyAccount/MyPantry")]
        public IActionResult Index(){
            var ingredients = _context.Ingredients.Include(i => i.IngredientCategory).AsQueryable();
            List<Ingredient> ingredientsList = ingredients.OrderBy(i => i.IngredientCategory.Name).ToList();
            return View(ingredientsList);
        }

        public IActionResult SaveIngredients(List<Ingredient> model){

            return View();
        }

    }
}