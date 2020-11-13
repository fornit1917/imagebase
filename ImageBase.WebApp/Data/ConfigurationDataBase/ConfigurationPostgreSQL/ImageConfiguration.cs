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
            entityBuilder.Property(i => i.Id).ValueGeneratedOnAdd();

            entityBuilder.ToTable("images");
        }
    }
}
