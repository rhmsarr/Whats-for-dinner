using Microsoft.AspNetCore.Mvc;

namespace WhatsForDinner.Controllers{
    public class RecipeController:Controller{

        public IActionResult RecipesList(){
            return View();
        }
        
    }
}