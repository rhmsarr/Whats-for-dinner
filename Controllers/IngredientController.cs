using Microsoft.AspNetCore.Mvc;
using WhatsForDinner.Models;

namespace WhatsForDinner.Controllers{

    public class IngredientController : Controller{

        [Route("MyAccount/MyPantry")]
        public IActionResult Index(){
            return View(IngredientService.ingredients);
        }

        public IActionResult SaveIngredients(List<Ingredient> model){

            return View();
        }

    }
}