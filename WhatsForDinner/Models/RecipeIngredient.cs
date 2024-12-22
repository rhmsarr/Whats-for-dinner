/*
 *      Rahma Toulaye Sarr - B231202551 - SWE203 Final Project
 */
namespace WhatsForDinner.Models{
    public class RecipeIngredient{

        public int RecipeIngredientId { get; set; }
        public string IngredientName { get; set;} = string.Empty;
        public int RecipeId { get; set;}
        public string quantity { get; set;} = string.Empty;

        public Recipe recipe { get; set;} = new Recipe();
    }
}