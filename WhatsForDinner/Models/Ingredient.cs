/*
 *      Rahma Toulaye Sarr - B231202551 - SWE203 Final Project
 */
namespace WhatsForDinner.Models{
    public class Ingredient{

        public int IngredientId { get; set; }
        public string Name{ get; set; } = String.Empty;

        
        public long IngredientCategoryId { get; set; }
        public IngredientCategory IngredientCategory { get; set; } = new IngredientCategory();
        
        
    }
}