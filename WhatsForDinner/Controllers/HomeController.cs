using Microsoft.AspNetCore.Mvc;

namespace WhatsForDinner.Controllers{
    public class HomeController : Controller{

        public IActionResult Index(){
            return View();
        }
    }
}