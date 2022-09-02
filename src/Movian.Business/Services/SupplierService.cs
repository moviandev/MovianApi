using System;
using System.Linq;
using System.Threading.Tasks;
using Movian.Business.Interfaces;
using Movian.Business.Models;
using Movian.Business.Validations;

namespace Movian.Business.Services
{
  public class SupplierService : BaseService, ISupplierService
  {
    private readonly ISupplierRepository _supplierRepository;
    private readonly IAddressRepository _addressRepository;

    public SupplierService(ISupplierRepository supplerRepository, IAddressRepository addressRepository)
    {
      _supplierRepository = supplerRepository;
      _addressRepository = addressRepository;
    }

    public async Task AddAsync(Supplier supplier)
    {
      if (!ExcuteValidation(new SupplierValidation(), supplier)
        || !ExcuteValidation(new AddressValidation(), supplier.Address))
        return;

      if (_supplierRepository.SearchAsync(f => f.Document == supplier.Document).Result.Any())
      {
        // Add notifications
        return;
      }

      await _supplierRepository.AddAsync(supplier);
    }

    public async Task UpdateAsync(Supplier supplier)
    {
      if (!ExcuteValidation(new SupplierValidation(), supplier))
        return;

      if (_supplierRepository.SearchAsync(f => f.Document == supplier.Document && f.Id != supplier.Id).Result.Any())
      {
        // Add notification
        return;
      }

      await _supplierRepository.AddAsync(supplier);
    }

    public async Task UpdateAddressAsync(Address address)
    {
      if (!ExcuteValidation(new AddressValidation(), address))
        return;

      await _addressRepository.UpdateAsync(address);
    }

    public async Task RemoveAsync(Guid id)
    {
      if (_supplierRepository.GetSupplierAndAddressAndProducts(id).Result.Products.Any())
      {
        // Add notification
        return;
      }

      var address = await _addressRepository.GetAddressBySupplierId(id);

      await _supplierRepository.RemoveAsync(id);

      return;
    }

    public void Dispose()
    {
      _supplierRepository?.Dispose();
      _addressRepository?.Dispose();
    }
  }
}