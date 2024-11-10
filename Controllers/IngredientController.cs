using Microsoft.AspNetCore.Mvc; 

namespace WhatsForDinner.Controllers{

    public class IngredientController : Controller{

        [Route("MyAccount/MyPantry")]
        public IActionResult Ingredients(){
            return View(IngredientService.ingredients);
        }
    }
}