
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
        public async Task<List<GeneratedRecipe>> GenerateRecipeWithIngredients(List<string> pantryIngredients){
            //prompt for the Cohere API to process and then generate a recipe in the Json format.
            string query = $@"Generate exactly two recipes that use the following ingredients: {string.Join(",", pantryIngredients)}. 
                            The JSON format must strictly follow these rules: 
                            - Only include the fields: ""RecipeName"", ""CookTime"", ""PrepTime"", ""Servings"", ""Ingredients"", and ""Steps"". 
                            - The ""Ingredients"" array should only contain objects with ""IngredientName"" and ""Quantity"".
                            - The ""Steps"" array should only contain objects with ""StepNumber"" and ""StepInstruction"".
                            - Do not include any other fields, commas, or extra characters.
                            - Ensure the JSON is correctly formatted and valid without extra fields or mistakes.";
            
            //preparing the request
            var requestBody = new{
                model = "command-r-plus", //model that will be used
                prompt = query, //prompt to be sent to the model
                max_tokens = 2048, //maximum length of the model's response and the prompt combined 
                temperature = 0.7 //fine tuning the model's temperature

            };

           // Serialize the request body into JSON
            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            // Prepare the HttpRequestMessage
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api.cohere.com/generate")
            {
                Content = content
            };

            // Add the Authorization header to the request
            //Console.WriteLine(_apikey);
            requestMessage.Headers.Add("Authorization", $"BEARER {_apikey}");

            var response = await _httpClient.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(responseBody);

            // The actual recipe JSON is inside the "text" field, which is a string.
            string recipeJson = result.text.ToString();
            recipeJson = CleanJsonString(recipeJson);
            recipeJson = HandleEscapeCharacters(recipeJson);
            //recipeJson = RemoveTrailingCommas(recipeJson);



            // Now, deserialize the recipe JSON string into the GeneratedRecipe object
            List<GeneratedRecipe> recipe = JsonConvert.DeserializeObject<List<GeneratedRecipe>>(recipeJson);

            return recipe;

        }
        private string RemoveTrailingCommas(string jsonString)
        {
            // Remove trailing commas from arrays (Ingredients and Steps)
            // Ingredients array
            jsonString = Regex.Replace(jsonString, @"(\[.*?)(,)(\s*?\])", "$1$3");

            // Steps array
            jsonString = Regex.Replace(jsonString, @"(\{.*?)(,)(\s*?\})", "$1$3");

            return jsonString;
        }
        
        private string HandleEscapeCharacters(string jsonString)
        {
            // Remove any stray backslashes before we deserialize
            // Ensure that escape sequences are valid
            StringBuilder sb = new StringBuilder(jsonString);

            // Remove any unnecessary backslashes that could break the JSON structure
            sb.Replace("\\\\", "\\"); // Replace double backslashes with single backslashes
            sb.Replace("\\\"", "\""); // Replace escaped quotes with regular quotes

            return sb.ToString();
        }
          private string CleanJsonString(string jsonString)
            {
                // Remove the opening and closing triple backticks and any extra whitespace around the JSON
                string cleanedJson = jsonString.Trim();

                // If the JSON string is wrapped in triple backticks (markdown format), remove them
                if (cleanedJson.StartsWith("```json"))
                {
                    cleanedJson = cleanedJson.Substring(7).Trim(); // Remove the "```json" part
                }

                if (cleanedJson.EndsWith("```"))
                {
                    cleanedJson = cleanedJson.Substring(0, cleanedJson.Length - 3).Trim(); // Remove the closing "```"
                }

                return cleanedJson;
            }
        
       
    }
        
}
