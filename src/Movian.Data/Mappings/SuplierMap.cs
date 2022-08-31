using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movian.Business.Models;

namespace Movian.Data.Mappings
{
  public class SuplierMap : IEntityTypeConfiguration<Suplier>
  {
    public void Configure(EntityTypeBuilder<Suplier> builder)
    {
      builder
        .HasKey(p => p.Id)
        .HasName("Id_Suplier");

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
        .HasColumnType("varchar(2)")
        .IsRequired();

      builder.Property(p => p.Active)
        .HasColumnType("integer")
        .IsRequired();

      builder.HasOne(p => p.Address)
        .WithOne(p => p.Suplier);

      builder.HasMany(p => p.Products)
        .WithOne(p => p.Suplier)
        .HasForeignKey(p => p.SuplierId);

      builder.ToTable("TB_Suplier");
    }
  }

}