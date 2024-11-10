using Microsoft.AspNetCore.Mvc; 

namespace WhatsForDinner.Controllers{

    public class IngredientController : Controller{

        [Route("MyAccount/MyPantry")]
        public IActionResult Index(){
            return View(IngredientService.ingredients);
        }

        public IActionResult Category(string category){

            return View(IngredientService.GetByCategory(category));
        }

    }
}