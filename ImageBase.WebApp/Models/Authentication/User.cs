using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Models.Authentication
{
    public class User : IdentityUser, IUser
    {
    }

}
