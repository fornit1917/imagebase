using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Data.ConfigurationDataBase.ConfigurationPostgreSQL
{
    public class IdentityRoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
        {
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.RoleId).HasColumnName("role_id");
            builder.Property(e => e.ClaimType).HasColumnName("claim_type");
            builder.Property(e => e.ClaimValue).HasColumnName("claim_value");
            builder.ToTable("roles_claims");
        }
    }
}
