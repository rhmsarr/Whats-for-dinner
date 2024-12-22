
/*
 *      Rahma Toulaye Sarr - B231202551 - SWE203 Final Project
 */
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WhatsForDinner.Models;
using WhatsForDinner.ViewModels;

namespace WhatsForDinner.Services{
    public class CohereService{
        private readonly HttpClient _httpClient;
        private readonly string _apikey;
        private readonly DinnerDbContext _context;

        public CohereService(HttpClient httpClient, IOptions<CohereSettings> cohereSettings, DinnerDbContext context) {
            _httpClient = httpClient;
            _apikey = cohereSettings.Value.ApiKey;
            _context = context;
        }
        //method creating an http request to https://api.cohere.com/generate for the command r + model to generate recipes
        public async Task<List<GeneratedRecipe>> GenerateRecipeWithIngredients(List<string> pantryIngredients){
            //explanatory prompt for the Cohere API to process and then generate a recipe in the Json format.
            string query = $@"
                Generate exactly two recipes that use the following ingredients: {string.Join(",", pantryIngredients)}. 
                The JSON format must strictly follow these rules:

                1. Only include the following fields: ""RecipeName"", ""CookTime"", ""PrepTime"", ""Servings"", ""Ingredients"", and ""Steps"".
                2. The ""Ingredients"" array must contain only objects with these exact two fields:
                - ""IngredientName"": a string (the name of the ingredient)
                - ""Quantity"": a string or number (the amount of the ingredient, e.g. '2 small', '1 cup', etc.)
                Example of the correct format:
                [{{""IngredientName"": ""Sweet Potato"", ""Quantity"": ""2 small""}}]
                3. The ""Steps"" array must contain only objects with these exact two fields:
                - ""StepNumber"": an integer (the step number, e.g. 1, 2, 3, etc.)
                - ""StepInstruction"": a string (the detailed step instruction)
                Example of the correct format:
                [{{""StepNumber"": 1, ""StepInstruction"": ""Preheat the oven to 400°F."" }}]
                4. Do not include any other fields, such as ""BoundingBox"", ""Coefficient"", ""Description"", or anything else.
                5. Do not change the names of the fields or add new ones.
                6. Ensure that there are no extra fields or unexpected characters in the output. The JSON must be **valid** and follow the exact format outlined above.

                **Important:**
                - The ""Ingredients"" array must only contain the fields ""IngredientName"" and ""Quantity"" with **no extra fields**.
                - The ""Steps"" array must only contain the fields ""StepNumber"" and ""StepInstruction"".
                - Only include the required fields listed above in the JSON, no extra fields or characters.
                - Output only the valid JSON, with no explanations, extra characters, or additional comments.
                ";

            
            //preparing the request
            var requestBody = new{
                model = "command-r-plus", //model that will be used
                prompt = query, //prompt to be sent to the model
                max_tokens = 2048, //maximum length of the model's response and the prompt combined 
                temperature = 0.7 //fine tuning the model's temperature

            };

           //serializing the request body into JSON
            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            //preparing the HttpRequestMessage
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api.cohere.com/generate")
            {
                Content = content
            };

            //adding the Authorization header to the request
            requestMessage.Headers.Add("Authorization", $"BEARER {_apikey}");

            var response = await _httpClient.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(responseBody);

            // retrieving the actual recipe inside the text field
            string recipeJson = result.text.ToString();
            recipeJson = CleanJsonString(recipeJson);
            recipeJson = HandleEscapeCharacters(recipeJson);



            //deserializing the recipe JSON string into the GeneratedRecipe object
            List<GeneratedRecipe> recipe = JsonConvert.DeserializeObject<List<GeneratedRecipe>>(recipeJson);

            return recipe;

        }
        
        
        private string HandleEscapeCharacters(string jsonString)
        {
            //removing any stray backslashes before we deserialize
            //ensuring that escape sequences are valid
            StringBuilder sb = new StringBuilder(jsonString);

            //removing any unnecessary backslashes that could break the JSON structure
            sb.Replace("\\\\", "\\"); //replacing double backslashes with single backslashes
            sb.Replace("\\\"", "\""); //replacing escaped quotes with regular quotes

            return sb.ToString();
        }
          private string CleanJsonString(string jsonString)
            {
                //removing the opening and closing triple backticks and any extra whitespace around the JSON
                string cleanedJson = jsonString.Trim();

                //if the JSON string is wrapped in triple backticks (markdown format), removing them
                if (cleanedJson.StartsWith("```json"))
                {
                    cleanedJson = cleanedJson.Substring(7).Trim(); //removing the "```json" part
                }

                if (cleanedJson.EndsWith("```"))
                {
                    cleanedJson = cleanedJson.Substring(0, cleanedJson.Length - 3).Trim(); //removing the closing "```"
                }

                return cleanedJson;
            }
        
       
    }
        
}
