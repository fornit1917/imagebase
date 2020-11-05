using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Dtos.AuthenticationDto
{
    public class RegisterUserDto
    {
        [EmailAddress]
        [RegularExpression(@"\S+@\S+\.\S+", ErrorMessage = "Characters are not allowed")]
        [Required(ErrorMessage = "Email must be required")]
        public string Email { get; set; }

        [MinLength(6, ErrorMessage = "String length must be more than 6 characters")]
        [Required(ErrorMessage = "Password must be required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password must be required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string PasswordConfirm { get; set; }
    }
}
