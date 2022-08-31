using System;
using System.Threading.Tasks;
using Movian.Business.Models;

namespace Movian.Business.Interfaces
{
  public interface ISupplierRepository : IRepository<Supplier>
  {
    Task<Supplier> GetSupplierAndAddress(Guid id);
    Task<Supplier> GetSupplierAndAddressAndProducts(Guid id);
  }
}