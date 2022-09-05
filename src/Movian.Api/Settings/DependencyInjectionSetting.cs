using Microsoft.Extensions.DependencyInjection;
using Movian.Business.Interfaces;
using Movian.Business.Notifications;
using Movian.Business.Services;
using Movian.Data.Contexts;
using Movian.Data.Repositories;

namespace Movian.Api.Settings
{
  public static class DepencencyInjectionSetting
  {
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
      services.AddScoped<AppDbContext>();
      services.AddScoped<IProductRepository, ProductRepository>();
      services.AddScoped<ISupplierRepository, SupplierRepository>();
      services.AddScoped<IAddressRepository, AddressRepository>();

      services.AddScoped<IProductService, ProductService>();
      services.AddScoped<ISupplierService, SupplierService>();
      services.AddScoped<INotifier, Notifier>();

      return services;
    }
  }
}