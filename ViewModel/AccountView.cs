using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Practice4.Models;
using System.ComponentModel.DataAnnotations;
namespace Practice4.ViewModel
{
    public class AccountView
    {
        [Required(ErrorMessage = "username is required")]
        [StringLength(5, ErrorMessage = "Must be between 3 and 5 characters", MinimumLength = 3)]
        public string Username { get; set; }

       
        [Required(ErrorMessage = "Password is required")]
        [StringLength(5, ErrorMessage = "Must be between 1 and 5 characters", MinimumLength = 1)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(5, ErrorMessage = "Must be between 1 and 5 characters", MinimumLength = 1)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}