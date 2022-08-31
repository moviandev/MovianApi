using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movian.Business.Interfaces;
using Movian.Business.Models;
using Movian.Data.Contexts;

namespace Movian.Data.Repositories
{
  public class SupplierRepository : Repository<Supplier>, ISupplierRepository
  {
    public SupplierRepository(AppDbContext db) : base(db)
    {
    }

    public async Task<Supplier> GetSupplierAndAddress(Guid id)
    {
      return await Db
        .Suppliers
        .Include(o => o.Address)
        .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Supplier> GetSupplierAndAddressAndProducts(Guid id)
    {
      return await Db
        .Suppliers
        .Include(s => s.Address)
        .Include(s => s.Products)
        .FirstOrDefaultAsync(s => s.Id == id);
    }
  }
}