using Microsoft.AspNetCore.Mvc;

namespace WhatsForDinner.Controllers{
    public class RecipeController:Controller{

        public IActionResult Generate(){
            return View();
        }

        public IActionResult RecipesList(){
            return View();
        }
        
    }
}