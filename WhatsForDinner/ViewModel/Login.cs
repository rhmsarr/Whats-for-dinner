using System.ComponentModel.DataAnnotations;

namespace WhatsForDinner.ViewModels{
    public class Login{
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}