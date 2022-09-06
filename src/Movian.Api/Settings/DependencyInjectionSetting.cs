using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Movian.Api.Extensions;
using Movian.Business.Interfaces;
using Movian.Business.Notifications;
using Movian.Business.Services;
using Movian.Data.Contexts;
using Movian.Data.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;

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

      services.AddScoped<INotifier, Notifier>();
      services.AddScoped<IProductService, ProductService>();
      services.AddScoped<ISupplierService, SupplierService>();

      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddScoped<IUser, AspNetUser>();
      services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfig>();

      return services;
    }
  }
}