using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Data.ConfigurationDataBase.ConfigurationPostgreSQL
{
    public class IdentityUserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
        {
            builder.Property(e => e.UserId).HasColumnName("id");
            builder.Property(e => e.LoginProvider).HasColumnName("login_provider");
            builder.Property(e => e.Name).HasColumnName("name");
            builder.Property(e => e.Value).HasColumnName("value");
            builder.ToTable("users_tokens");
        }
    }
}
