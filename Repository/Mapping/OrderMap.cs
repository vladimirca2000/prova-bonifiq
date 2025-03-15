using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProvaPub.Models;

namespace ProvaPub.Repository.Mapping
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(o => o.Value)
                .HasColumnName("Value")
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            builder.Property(o => o.CustomerId)
                .HasColumnName("CustomerId")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(o => o.OrderDate)
                .HasColumnName("OrderDate")
                .HasColumnType("datetime2")
                .IsRequired();

            builder.HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId);
        }
    }
}
