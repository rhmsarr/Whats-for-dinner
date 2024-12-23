/*
 *      Rahma Toulaye Sarr - B231202551 - SWE203 Final Project
 */

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
        //method returning the login page and saving the url the user came from to then redirect them
        public IActionResult Login(string returnUrl = "/"){
            return View(new Login { returnUrl = returnUrl });
        }
        [HttpPost]
        //method logging the user in, redirecting them to the page they came from
        public async Task<IActionResult> Login(Login credentials){
            credentials.returnUrl = credentials.returnUrl ?? "/";
            //if the user entered credentials in the form
            if (ModelState.IsValid){
                var user = await _userManager.FindByEmailAsync(credentials.Email);
                //if the entered email is actually related to an already registered user
                if (user != null){
                    //checking if the password is valid
                    var loginResult = await _signInManager.PasswordSignInAsync(user, credentials.Password, false, false);
                    if(loginResult.Succeeded){
                        
                        //returning to the page the user came from.
                        return Redirect(credentials.returnUrl);
                    } 
                }
                
            }
            ModelState.AddModelError(string.Empty, "The email or the password was incorrectly entered.");
            return View(credentials);
        }
        [HttpGet]
        //method returning the sign up view
        public IActionResult SignUp(){
            return View();
        }
        [HttpPost]
        //method signing up the user
        public async Task<IActionResult> SignUp(SignUp credentials){

            //if the user entered valid information
            if(ModelState.IsValid){
                //checking if an account has already been created with this email
                var existingUser = await _userManager.FindByEmailAsync(credentials.Email);
                if(existingUser != null){
                    
                    ModelState.AddModelError(string.Empty, "An account with this email already exists.");
                    return View(credentials);
                }
                //creating the user
                var user = new IdentityUser{
                    UserName = credentials.Email,
                    Email = credentials.Email
                };
                var result = await _userManager.CreateAsync(user, credentials.Password);
                //redirecting the user to login
                if(result.Succeeded){
                    return RedirectToAction("Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
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