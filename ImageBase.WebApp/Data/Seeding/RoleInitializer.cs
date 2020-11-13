using ImageBase.WebApp.Data.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Data.Seeding
{
    public class RoleInitializer
    {
        public static async Task InitializeDBAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@mail.ru";
            string password = "12345ABcd!";
            if (await roleManager.FindByNameAsync(UserRoles.Admin) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
            if (await roleManager.FindByNameAsync(UserRoles.User) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, UserRoles.Admin);
                }
            }
        }
    }
}
