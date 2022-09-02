using System;
using System.Threading.Tasks;
using Movian.Business.Models;

namespace Movian.Business.Interfaces
{
  public interface IProductService : IDisposable
  {
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task RemoveAsync(Guid id);
  }
}