using ImageBase.WebApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Data.ConfigurationDataBase.ConfigurationPostgreSQL
{
    public class ImageCatalogConfiguration
    {
        public ImageCatalogConfiguration(EntityTypeBuilder<ImageCatalog> entityBuilder)
        {
            entityBuilder.HasKey(ic => new { ic.CatalogId, ic.ImageId});
            entityBuilder.Property(ic => ic.ImageId).HasColumnName("image_id");
            entityBuilder.Property(ic => ic.CatalogId).HasColumnName("catalog_id");

            entityBuilder.HasOne(i => i.Image)
                .WithMany(ic => ic.ImageCatalogs)
                .HasForeignKey(ic => ic.ImageId);

            entityBuilder.HasOne(c => c.Catalog)
                .WithMany(ic => ic.ImageCatalogs)
                .HasForeignKey(ic => ic.CatalogId);

            entityBuilder.ToTable("images_catalogs");
        }
    }
}
