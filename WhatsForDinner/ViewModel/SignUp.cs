using System.ComponentModel.DataAnnotations;

namespace WhatsForDinner.ViewModels{
    public class SignUp : Login{
        [Required]
        [Compare("Password", ErrorMessage = "The password do not match.")]

        public string ConfirmPassword { get; set; }
        
    }
}