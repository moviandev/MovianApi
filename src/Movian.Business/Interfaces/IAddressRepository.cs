using System;
using System.Threading.Tasks;
using Movian.Business.Models;

namespace Movian.Business.Interfaces
{
  public interface IAddressRepository : IRepository<Address>
  {
    Task<Address> GetAddressBySupplierId(Guid supplierId);
  }
}