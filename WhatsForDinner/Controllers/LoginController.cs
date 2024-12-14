using Microsoft.AspNetCore.Mvc;

namespace WhatsForDinner.Controllers{
    public class LoginController : Controller{
        public IActionResult Index(){
            return View();
        }
    }
}