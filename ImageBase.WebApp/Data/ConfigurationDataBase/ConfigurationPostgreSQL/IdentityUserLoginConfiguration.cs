using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Data.ConfigurationDataBase.ConfigurationPostgreSQL
{
    public class IdentityUserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
        {
            builder.Property(e => e.LoginProvider).HasColumnName("login_provider");
            builder.Property(e => e.ProviderKey).HasColumnName("provider_key");
            builder.Property(e => e.ProviderDisplayName).HasColumnName("provider_display_name");
            builder.Property(e => e.UserId).HasColumnName("user_id");
            builder.ToTable("users_logins");
        }
    }
}
