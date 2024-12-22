using Microsoft.AspNetCore.Mvc;
using WhatsForDinner.ViewModels;


using Microsoft.AspNetCore.Identity;

namespace WhatsForDinner.Controllers{
    public class AccountController : Controller{
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private const string _user = "user";

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager){
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "/"){
            return View(new Login { returnUrl = returnUrl });
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login credentials){
            credentials.returnUrl = credentials.returnUrl ?? "/";
            if (ModelState.IsValid){
                var user = await _userManager.FindByEmailAsync(credentials.Email);
                if (user != null){
                    var loginResult = await _signInManager.PasswordSignInAsync(user, credentials.Password, false, false);
                    if(loginResult.Succeeded){
                        

                        return Redirect(credentials.returnUrl);
                    } 
                }
                
            }
            ModelState.AddModelError(string.Empty, "The email or the password was incorrectly entered.");
            return View(credentials);
        }
        [HttpGet]
        public IActionResult SignUp(){
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUp credentials){

            if(ModelState.IsValid){
                var existingUser = await _userManager.FindByEmailAsync(credentials.Email);
                if(existingUser != null){
                     ModelState.AddModelError(string.Empty, "An account with this email already exists.");
                    return View(credentials);
                }

                var user = new IdentityUser{
                    UserName = credentials.Email,
                    Email = credentials.Email
                };
                var result = await _userManager.CreateAsync(user, credentials.Password);

                if(result.Succeeded){
                    return RedirectToAction("Login");
                }
            }
            return View(credentials);
            
        }
        public async Task<IActionResult> Logout(){
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }
    }
}