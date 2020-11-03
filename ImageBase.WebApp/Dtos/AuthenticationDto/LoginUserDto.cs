using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Dtos.AuthenticationDto
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "You have not entered your email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You have not entered your password")]
        public string Password { get; set; }

    }
}
