using System;
using System.Threading.Tasks;
using Movian.Business.Models;

namespace Movian.Business.Interfaces
{
  public interface ISupplierService : IDisposable
  {
    Task AddAsync(Supplier supplier);
    Task UpdateAsync(Supplier supplier);
    Task UpdateAddressAsync(Address supplier);
    Task RemoveAsync(Guid id);
  }
}