using AutoMapper;
using Movian.Api.Models;
using Movian.Business.Models;

namespace Movian.Api.Settings
{
  public class AutoMapperSetting : Profile
  {
    public AutoMapperSetting()
    {
      CreateMap<Product, ProductDto>().ReverseMap();
      CreateMap<Product, ProductImageDto>().ReverseMap();
      CreateMap<Supplier, SupplierDto>().ReverseMap();
      CreateMap<Address, AddressDto>().ReverseMap();

      CreateMap<Product, ProductDto>()
        .ForMember(p => p.SupplierName,
                   t => t.MapFrom(s => s.Supplier.Name));
    }
  }
}