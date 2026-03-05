using Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Dtata.Configuration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.ProductBrand)
                 .WithMany()
                 .HasForeignKey(p => p.ProductBrandId);

            builder.HasOne(p => p.ProductType)
                .WithMany()
                .HasForeignKey(p => p.ProductTypeId);

            builder.Property(p=>p.Name).IsRequired();
            builder.Property(p=> p.Description).IsRequired();
            builder.Property(p=>p.PictureUrl).IsRequired();
      //      builder.Property(p => p.price).HasColumnType("decimal(18,2)");
        }
    }
}
