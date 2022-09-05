using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movian.Api.Controllers;
using Movian.Api.Models;
using Movian.Business.Interfaces;
using Movian.Business.Models;

namespace Movian.Api.Versions.V1.Controllers
{
  [Route("api/[controller]")]
  public class SupplierController : BaseController
  {
    private readonly ISupplierService _service;
    private readonly ISupplierRepository _supplierRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;

    public SupplierController(INotifier notifier,
                              ISupplierService service,
                              ISupplierRepository supplierRepository,
                              IAddressRepository addressRepository,
                              IMapper mapper) : base(notifier)
    {
      _service = service;
      _supplierRepository = supplierRepository;
      _addressRepository = addressRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SupplierDto>>> GetAll()
    {
      var allSuppliers = _mapper.Map<IEnumerable<SupplierDto>>(await _supplierRepository.GetAllAsync());
      return CustomResponse(allSuppliers);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SupplierDto>> GetById(Guid id)
    {
      var supplier = await GetSupplierAndAddressAndProducts(id);
      return CustomResponse(supplier);
    }

    [HttpPost]
    public async Task<ActionResult<SupplierDto>> Add(SupplierDto supplier)
    {
      if (!ModelState.IsValid) CustomResponse(ModelState);

      await _service.AddAsync(_mapper.Map<Supplier>(supplier));

      return CustomResponse(supplier);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<SupplierDto>> Update(Guid id, [FromBody] SupplierDto supplier)
    {
      if (id != supplier.Id)
      {
        NotifyError("The inputed Id in the URI is not the same of the DTO try again");
        CustomResponse();
      }

      if (!ModelState.IsValid) CustomResponse(ModelState);

      await _service.UpdateAsync(_mapper.Map<Supplier>(supplier));

      return CustomResponse(supplier);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> Delete(Guid id)
    {
      var supplier = await GetSupplierAndAddress(id);

      if (supplier is null) return NotFound();

      await _service.RemoveAsync(id);

      return CustomResponse(id);
    }

    [HttpGet("get-address/{id:guid}")]
    public async Task<ActionResult<AddressDto>> GetAddress(Guid id)
    {
      var address = _mapper.Map<AddressDto>(await _addressRepository.GetByIdAsync(id));
      return CustomResponse(address);
    }

    [HttpPut("update-address/{id:guid}")]
    public async Task<ActionResult<AddressDto>> UpdateAddress(Guid id, [FromBody] AddressDto address)
    {
      if (id != address.Id)
      {
        NotifyError("The inputed Id in the URI is not the same of the DTO try again");
        CustomResponse();
      }

      if (!ModelState.IsValid) CustomResponse(ModelState);

      await _service.UpdateAddressAsync(_mapper.Map<Address>(address));

      return CustomResponse(address);
    }

    private async Task<SupplierDto> GetSupplierAndAddressAndProducts(Guid id)
      => _mapper.Map<SupplierDto>(await _supplierRepository.GetSupplierAndAddressAndProducts(id));

    private async Task<SupplierDto> GetSupplierAndAddress(Guid id)
      => _mapper.Map<SupplierDto>(await _supplierRepository.GetSupplierAndAddress(id));
  }
}