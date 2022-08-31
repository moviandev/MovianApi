using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movian.Business.Models;

namespace Movian.Data.Mappings
{
  public class SuplierMap : IEntityTypeConfiguration<Supplier>
  {
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
      builder
        .HasKey(p => p.Id)
        .HasName("Id_Supplier");

      builder
        .Property(p => p.Id)
        .IsRequired()
        .HasColumnName("Id_Supplier");

      builder
        .Property(p => p.AddressId)
        .HasColumnName("Id_Address")
        .IsRequired();

      builder
        .Property(p => p.Name)
        .HasColumnType("varchar(200)")
        .IsRequired();

      builder
        .Property(p => p.Document)
        .HasColumnType("varchar(14)")
        .IsRequired();

      builder
        .Property(p => p.SuplierType)
        .HasColumnType("INTEGER")
        .IsRequired();

      builder.Property(p => p.Active)
        .HasColumnType("integer")
        .IsRequired();

      builder.HasOne(p => p.Address)
        .WithOne(p => p.Supplier)
        .HasForeignKey<Address>(p => p.SupplierId);

      builder.HasMany(p => p.Products)
        .WithOne(p => p.Supplier)
        .HasForeignKey(p => p.SupplierId);

      builder.ToTable("TB_Supplier");
    }
  }

}