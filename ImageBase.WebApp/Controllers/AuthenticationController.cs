using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using ImageBase.WebApp.Data.Dtos.AuthenticationDto;
using ImageBase.WebApp.Data.Models.Authentication;
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
    [Controller]
    public class AuthenticationController : Controller
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

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserDto model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
            {
                ModelState.AddModelError("UserExcisting", "User already exists!");
                return View(model);
            }
            if (ModelState.IsValid)
            {               
                User user = new User()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.PasswordSignInAsync(user.Email, model.Password, true, false);
                    await _userManager.AddToRoleAsync(user, UserRoles.User);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }                           
            return View(model);
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUserDto model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Unauthorized", "Credentials of user are invalid");
                }
            }
            return View(model);
            
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
