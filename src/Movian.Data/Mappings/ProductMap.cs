using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movian.Business.Models;

namespace Movian.Data.Mappings
{
  public class ProductMap : IEntityTypeConfiguration<Product>
  {
    public void Configure(EntityTypeBuilder<Product> builder)
    {
      builder
        .HasKey(p => p.Id)
        .HasName("Id_Product");

      builder
        .Property(p => p.Id)
        .IsRequired()
        .HasColumnName("Id_Product");

      builder.Property(p => p.SuplierId)
        .IsRequired()
        .HasColumnName("Id_Supplier");

      builder.Property(p => p.Name)
        .IsRequired()
        .HasColumnType("varchar(200)");

      builder.Property(p => p.Description)
        .IsRequired()
        .HasColumnType("varchar(1000)");


      builder.Property(p => p.Image)
        .IsRequired()
        .HasColumnType("varchar(100)");

      builder.Property(p => p.Active)
        .HasColumnType("integer")
        .IsRequired();

      builder.HasOne(p => p.Suplier)
        .WithMany(p => p.Products);

      builder.ToTable("TB_Products");
    }
  }
}