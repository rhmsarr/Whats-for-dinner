/*
 *      Rahma Toulaye Sarr - B231202551 - SWE203 Final Project
 */
using System.ComponentModel.DataAnnotations;

namespace WhatsForDinner.ViewModels{
    public class Login{
        [Required(ErrorMessage = "Please enter your email.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter your password.")]
        public string Password { get; set; } = string.Empty;    
        public string returnUrl { get; set; } = string.Empty;
    }
}