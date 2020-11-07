using ImageBase.WebApp.Data;
using ImageBase.WebApp.Data.ConfigurationDataBase.ConfigurationPostgreSQL;
using ImageBase.WebApp.Models.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.IntegrationTests.DataTests
{
    public class AspPostrgreSQLContextTest : IdentityDbContext<User>
    {
        public AspPostrgreSQLContextTest(DbContextOptions<AspPostrgreSQLContextTest> options)
          : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            Database.EnsureCreated();
            Database.Migrate();
            BuildPostgresqlConfiguration(builder);
        }
        private void BuildPostgresqlConfiguration(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new IdentityUserConfiguration());
            builder.ApplyConfiguration(new IdentityRoleConfiguration());
            builder.ApplyConfiguration(new IdentityUserRoleConfiguration());
            builder.ApplyConfiguration(new IdentityUserTokenConfiguration());
            builder.ApplyConfiguration(new IdentityRoleClaimConfiguration());
            builder.ApplyConfiguration(new IdentityUserLoginConfiguration());
            builder.ApplyConfiguration(new IdentityUserClaimConfiguration());

        }
    }
}
