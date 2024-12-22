/*
 *      Rahma Toulaye Sarr - B231202551 - SWE203 Final Project
 */

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhatsForDinner.Models;

namespace WhatsForDinner.Controllers{

    public class IngredientController : Controller{

        private readonly DinnerDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public IngredientController(DinnerDbContext context, UserManager<IdentityUser> userManager){
            _context = context;
            _userManager = userManager;
        }


        [Route("MyAccount/MyPantry")]
        //method returning the ingredients present in the database and the ingredients selected by the user
        public IActionResult Index(){
            //retrieving the ingredients from the database
            var ingredients = _context.Ingredients.Include(i => i.IngredientCategory).AsQueryable();
            List<Ingredient> ingredientsList = ingredients.OrderBy(i => i.IngredientCategory.Name).ToList();
            string? userID = _userManager.GetUserId(User);

            //if the user is logged in, retrieving the ingredients they have already selected
            List<string> selectedIngredients = new List<string>();
            if (userID != null)
            {
                selectedIngredients = _context.UsersIngredients
                    .Where(i => i.UserId == userID)
                    .Select(i => i.ingredient.Name)
                    .ToList();
            }

            //passing selected ingredients to the view
            ViewBag.SelectedIngredients = selectedIngredients;
            return View(ingredientsList);
        }

        [HttpPost]
        //method saving ingredients the user selected to the UserIngredient relation
        public IActionResult SaveIngredients(List<string> SelectedIngredients, string CategoryName){
            
            string? userID = _userManager.GetUserId(User);
            //the user has to be logged in
            if(userID == null){
                return RedirectToAction("Login", "Account",new{returnUrl = "/Ingredient/SaveIngredients"});
            }
            //list storing the ingredients the user already ahs saved.
            List<string> savedIngNames = _context.UsersIngredients.Include(i=>i.ingredient).Where(i => i.UserId == userID)
                .Select(i => i.ingredient.Name).ToList();
            //list checking if any ingredients that have been checked are not present in the database
            List<string> ingredientsToAdd = SelectedIngredients.Except(savedIngNames).ToList();
            //list checking if any ingredient present in the database have been unchecked by the user
            List<string> ingredientsToRemove = savedIngNames.Except(SelectedIngredients).ToList();

                //updating the database depending on the user input
                foreach(string ing in ingredientsToAdd){
                    IngredientUser ingredient = new IngredientUser{UserId = userID, IngredientId = _context.Ingredients.Where(i => i.Name == ing).FirstOrDefault().IngredientId, ingredient = null};
                    _context.UsersIngredients.Add(ingredient);
                }
                foreach(string ing in ingredientsToRemove){
                   var ingredient = _context.UsersIngredients.Where(i => i.ingredient.Name == ing && i.UserId == userID).FirstOrDefault();
                   _context.UsersIngredients.Remove(ingredient);

                
                }
            
            _context.SaveChanges();


            return RedirectToAction("Index");
        }

    }
}