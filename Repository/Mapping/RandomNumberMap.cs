using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProvaPub.Models;

namespace ProvaPub.Repository.Mapping;

public class RandomNumberMap : IEntityTypeConfiguration<RandomNumber>
{
    public void Configure(EntityTypeBuilder<RandomNumber> builder)
    {
        builder.ToTable("Numbers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasColumnName("Id")
            .HasColumnType("int");
        builder.Property(x => x.Number).IsRequired();
    }
}
