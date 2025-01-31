/*
 *      Rahma Toulaye Sarr - B231202551 - SWE203 Final Project
 */
namespace WhatsForDinner.Models{
    public class RecipeStep{
        public int RecipeStepId { get; set; }
        public int RecipeId { get; set; }
        public int StepNumber { get; set; }
        public string StepInstruction   { get; set; } = "";

        public Recipe recipe { get; set; } = new Recipe();

    }
}