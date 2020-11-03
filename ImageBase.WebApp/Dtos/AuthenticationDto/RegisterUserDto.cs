using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Dtos.AuthenticationDto
{
    public class RegisterUserDto
    {
        public string Email { get; set; }

        [MinLength(6, ErrorMessage = "String length must be more than 6 characters")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string PasswordConfirm { get; set; }
    }
}
