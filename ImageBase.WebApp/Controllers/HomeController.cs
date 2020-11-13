using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageBase.WebApp.Data.Models.Authentication;
using ImageBase.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ImageBase.WebApp.Controllers
{
    [Route("/")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var roles = new List<string>();
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
                roles = (List<string>)await _userManager.GetRolesAsync(user);

            return View(roles);
        }
    }
}
