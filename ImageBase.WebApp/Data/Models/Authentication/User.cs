using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Data.Models.Authentication
{
    public class User : IdentityUser, IUser
    {
        public List<Catalog> Catalogs { get; set; }
    }

}
