using Microsoft.AspNetCore.Mvc;

namespace WhatsForDinner.Controllers{

    public class GenerateController: Controller{
        [Route("Generate")]
        public ActionResult Generate(){
            return View("Views/Recipe/Generated.cshtml");
        }
    }
}