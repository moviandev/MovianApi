using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movian.Business.Interfaces;
using Movian.Business.Models;
using Movian.Data.Contexts;

namespace Movian.Data.Repositories
{
  public class AddressRepository : Repository<Address>, IAddressRepository
  {
    public AddressRepository(AppDbContext db) : base(db)
    {
    }

    public async Task<Address> GetAddressBySupplierId(Guid supplierId)
    {
      return await Db
        .Addresses
        .AsNoTracking()
        .FirstOrDefaultAsync(a => a.SupplierId == supplierId);
    }
  }
}