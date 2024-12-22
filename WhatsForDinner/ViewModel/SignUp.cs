/*
 *      Rahma Toulaye Sarr - B231202551 - SWE203 Final Project
 */
using System.ComponentModel.DataAnnotations;

namespace WhatsForDinner.ViewModels{
    public class SignUp{
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty; 
        [Required]
        [Compare("Password", ErrorMessage = "The password do not match.")]

        public string ConfirmPassword { get; set; } = string.Empty;
        
    }
}