/*
 *      Rahma Toulaye Sarr - B231202551 - SWE203 Final Project
 */
using System.ComponentModel.DataAnnotations;

namespace WhatsForDinner.ViewModels{
    public class SignUp{
        [Required(ErrorMessage = "Please enter your email.")]
        [EmailAddress(ErrorMessage="Please enter a valid email.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a password.")]
        public string Password { get; set; } = string.Empty; 

        [Required]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
        
    }
}