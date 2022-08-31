using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movian.Business.Models;

namespace Movian.Data.Mappings
{
  public class AddressMap : IEntityTypeConfiguration<Address>
  {
    public void Configure(EntityTypeBuilder<Address> builder)
    {
      builder
        .HasKey(p => p.Id)
        .HasName("Id_Address");

      builder.Property(p => p.SuplierId)
        .HasColumnName("Id_Suplier")
        .IsRequired();

      builder.Property(p => p.CompleteAddress)
        .IsRequired()
        .HasColumnType("varchar(100)");

      builder.Property(p => p.City)
          .HasColumnType("varchar(100)");

      builder.Property(p => p.Number)
          .HasColumnType("varchar(50)");

      builder.Property(p => p.State)
        .HasColumnType("varchar(50)");

      builder.Property(p => p.ZipCode)
        .HasColumnType("varchar(8)");

      builder.Property(p => p.Neighborhood)
        .HasColumnType("varchar(100)");

      builder.Property(p => p.AdditionalAddressData)
        .HasColumnType("varchar(200)");

      builder.HasOne(p => p.Suplier)
        .WithOne(p => p.Address);

      builder.ToTable("TB_Address");
    }
  }
}