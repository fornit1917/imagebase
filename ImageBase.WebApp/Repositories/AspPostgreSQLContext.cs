using ImageBase.WebApp.Data.ConfigurationDataBase.ConfigurationPostgreSQL;
using ImageBase.WebApp.Data.Models;
using ImageBase.WebApp.Data.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Repositories
{
    public class AspPostgreSQLContext : IdentityDbContext<User>
    {
        public DbSet<Image> Images { get; set; }
        public DbSet<ImageCatalog> ImageCatalogs { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public AspPostgreSQLContext(DbContextOptions<AspPostgreSQLContext> options)
            : base(options)
        {
        }

        public AspPostgreSQLContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
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

            new ImageConfiguration(builder.Entity<Image>());
            new CatalogConfiguration(builder.Entity<Catalog>());
            new ImageCatalogConfiguration(builder.Entity<ImageCatalog>());

        }
    }
}
