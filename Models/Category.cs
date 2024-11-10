using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
namespace WhatsForDinner.Models{

    public class Category{
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        

    }
}