using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movian.Business.Interfaces;
using Movian.Business.Models;
using Movian.Data.Contexts;

namespace Movian.Data.Repositories
{
  public class ProductRepository : Repository<Product>, IProductRepository
  {
    public ProductRepository(AppDbContext db) : base(db)
    {
    }

    public async Task<Product> GetProductAndSupplierById(Guid id)
    {
      return await Db
        .Products
        .Include(p => p.Supplier)
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetProductsAndSupplier()
    {
      return await Db
        .Products
        .AsNoTracking()
        .Include(p => p.Supplier)
        .OrderBy(p => p.Name)
        .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsBySupplierId(Guid supplierId)
    {
      return await Db
        .Products
        .AsNoTracking()
        .Include(p => p.Supplier)
        .Where(p => p.SupplierId == supplierId)
        .ToListAsync();
    }
  }
}