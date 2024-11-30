using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
namespace WhatsForDinner.Models{

    public class IngredientCategory{
        [Key]
        public long IngredientCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        

    }
}