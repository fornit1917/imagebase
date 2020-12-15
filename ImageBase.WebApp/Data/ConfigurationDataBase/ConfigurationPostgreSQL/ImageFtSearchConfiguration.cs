using ImageBase.WebApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ImageBase.WebApp.Data.ConfigurationDataBase.ConfigurationPostgreSQL
{
    public class ImageFtSearchConfiguration
    {
        public ImageFtSearchConfiguration(EntityTypeBuilder<ImageFtSearch> entityBuilder)
        {
            entityBuilder.HasOne(ifts => ifts.Image).WithOne(i => i.ImageFtSearch).HasForeignKey<ImageFtSearch>(ifts => ifts.ImageId).OnDelete(DeleteBehavior.Cascade);
            entityBuilder.HasKey(ifts => ifts.ImageId);
            entityBuilder.Property(ifts => ifts.ImageId).HasColumnName("image_id").IsRequired();
            entityBuilder.Property(ifts => ifts.ImageVector).HasColumnName("image_vector").IsRequired();

            entityBuilder.ToTable("images_ft_search");
        }
    }
}
