using Microsoft.EntityFrameworkCore;
using Movian.Business.Models;

namespace Movian.Data.Contexts
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
      ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
      ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Suplier> Supliers { get; set; }

    // Prevent unmapped properties to be set with values to big
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      foreach (var property in modelBuilder.Model.GetEntityTypes()
          .SelectMany(e => e.GetProperties()
              .Where(p => p.ClrType == typeof(string))))
        property.SetColumnType("varchar(100)");

      modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

      foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

      base.OnModelCreating(modelBuilder);
    }

    // Set CreatedIn
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
      foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedIn") != null))
      {
        if (entry.State == EntityState.Added)
        {
          entry.Property("CreatedIn").CurrentValue = DateTime.Now;
        }

        if (entry.State == EntityState.Modified)
        {
          entry.Property("CreatedIn").IsModified = false;
        }
      }

      return base.SaveChangesAsync(cancellationToken);
    }
  }
}