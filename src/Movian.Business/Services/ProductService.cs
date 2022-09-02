using System;
using System.Threading.Tasks;
using Movian.Business.Interfaces;
using Movian.Business.Models;
using Movian.Business.Validations;

namespace Movian.Business.Services
{
  public class ProductService : BaseService, IProductService
  {
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository,
        INotifier notifier) : base(notifier)
    {
      _productRepository = productRepository;
    }

    public async Task AddAsync(Product product)
    {
      if (!ExcuteValidation(new ProductValidation(), product)) return;

      await _productRepository.AddAsync(product);
    }

    public async Task UpdateAsync(Product product)
    {
      if (!ExcuteValidation(new ProductValidation(), product)) return;

      await _productRepository.UpdateAsync(product);
    }

    public async Task RemoveAsync(Guid id)
    {
      await _productRepository.RemoveAsync(id);
    }

    public void Dispose()
    {
      _productRepository?.Dispose();
    }
  }
}