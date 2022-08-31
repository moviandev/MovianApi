using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Movian.Business.Models;

namespace Movian.Business.Interfaces
{
  public interface IProductRepository : IRepository<Product>
  {
    Task<IEnumerable<Product>> GetProductsBySupplierId(Guid supplierId);
    Task<IEnumerable<Product>> GetProductsAndSupplier();
    Task<Product> GetProductAndSupplierById(Guid id);
  }
}