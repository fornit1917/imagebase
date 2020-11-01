using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using ImageBase.WebApp.Dtos.AuthenticationDto;
using ImageBase.WebApp.Models.Authentication;
using ImageBase.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ImageBase.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IMapper _mapper;

        public AuthenticationController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager, ILogger<AuthenticationController> logger, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response { Status = "Error", Message = "User already exists!" });
            }
            User user = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email
            };            
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);
            if (result.Succeeded)
            {
                return Ok(new Response { Status = "Success", Message = "Login succeeded" });
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new Response { Status = "Error", Message = "Credentials of user are invalid" });
            }
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new Response { Status = "Success", Message = "Logout succeeded" });
        }
    }
}
