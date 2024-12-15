using Microsoft.AspNetCore.Mvc;
using WhatsForDinner.ViewModels;


using Microsoft.AspNetCore.Identity;

namespace WhatsForDinner.Controllers{
    public class LoginController : Controller{
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public LoginController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager){
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index(){
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Login credentials){
            if (ModelState.IsValid){
                var user = await _userManager.FindByEmailAsync(credentials.Email);
                if (user != null){
                    var loginResult = await _signInManager.PasswordSignInAsync(user, credentials.Password, false, false);
                    if(loginResult.Succeeded){
                        return Redirect("/");
                    } 
                }
                
            }
            ModelState.AddModelError(string.Empty, "The email or the password was incorrectly entered.");
            return View(credentials);
        }
        public async Task<IActionResult> Logout(){
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }
    }
}