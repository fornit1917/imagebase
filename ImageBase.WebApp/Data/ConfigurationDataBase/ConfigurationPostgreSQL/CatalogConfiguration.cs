using ImageBase.WebApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Data.ConfigurationDataBase.ConfigurationPostgreSQL
{
    public class CatalogConfiguration
    {
        public CatalogConfiguration(EntityTypeBuilder<Catalog> entityBuilder)
        {
            entityBuilder.HasKey(c => c.Id);
            entityBuilder.Property(c => c.Id).HasColumnName("id");
            entityBuilder.Property(c => c.Name).HasColumnName("name").HasMaxLength(30).IsRequired();
            entityBuilder.Property(c => c.ParentCatalogId).HasColumnName("parent_catalog_id");
            entityBuilder.Property(c => c.UserId).HasColumnName("user_id");
            entityBuilder.Property(c => c.Id).ValueGeneratedOnAdd();

            entityBuilder.ToTable("catalogs");
        }
    }
}
