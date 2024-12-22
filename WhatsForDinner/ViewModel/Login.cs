using System.ComponentModel.DataAnnotations;

namespace WhatsForDinner.ViewModels{
    public class Login{
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;    
        public string returnUrl { get; set; } = string.Empty;
    }
}