using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Data.Dtos.AuthenticationDto
{
    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
    }
}
