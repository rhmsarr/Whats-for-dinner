/*
 *      Rahma Toulaye Sarr - B231202551 - SWE203 Final Project
 */

using Microsoft.AspNetCore.Mvc;


namespace WhatsForDinner.Controllers{
    public class HomeController : Controller{
        //method returning to the home view
        public IActionResult Index(){
            return View();
        }
    }
}