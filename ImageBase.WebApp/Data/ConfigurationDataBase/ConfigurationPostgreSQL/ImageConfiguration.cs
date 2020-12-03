using ImageBase.WebApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBase.WebApp.Data.ConfigurationDataBase.ConfigurationPostgreSQL
{
    public class ImageConfiguration
    {
        public ImageConfiguration(EntityTypeBuilder<Image> entityBuilder)
        {
            entityBuilder.HasKey(i => i.Id);
            entityBuilder.Property(i => i.Id).HasColumnName("id");
            entityBuilder.Property(i => i.Title).HasColumnName("title").HasMaxLength(200).IsRequired();
            entityBuilder.Property(i => i.Description).HasColumnName("description").HasColumnType("text");
            entityBuilder.Property(i => i.KeyWords).HasColumnName("key_words").HasColumnType("text");
            entityBuilder.Property(i => i.ServiceId).HasColumnName("service_id").IsRequired();
            entityBuilder.Property(i => i.ExternalId).HasColumnName("external_id").HasColumnType("text");
            entityBuilder.Property(i => i.FileSize).HasColumnName("file_size").IsRequired();
            entityBuilder.Property(i => i.Height).HasColumnName("height").IsRequired();
            entityBuilder.Property(i => i.Width).HasColumnName("width").IsRequired();
            entityBuilder.Property(i => i.LargePreviewUrl).HasColumnName("large_preview_url").HasColumnType("text").IsRequired();
            entityBuilder.Property(i => i.SmallPreviewUrl).HasColumnName("small_preview_url").HasColumnType("text");
            entityBuilder.Property(i => i.OriginalUrl).HasColumnName("original_url").HasColumnType("text").IsRequired();
            entityBuilder.Property(i => i.Id).ValueGeneratedOnAdd();

            entityBuilder.HasIndex(i => new { i.ExternalId, i.ServiceId }).IsUnique();

            entityBuilder.ToTable("images");
        }
    }
}
