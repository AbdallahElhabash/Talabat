using Core.Entites.Order_Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(O => O.Status)

                   .HasConversion(OStatus => OStatus.ToString(), OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus));
            builder.Property(O => O.SubTotoal)

                   .HasColumnType("decimal(18,2)");

            builder.OwnsOne(O => O.ShippingAddress, SA => SA.WithOwner());

            builder.HasOne(O => O.DeliveryMethod)
                   .WithMany()
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
