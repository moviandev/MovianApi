using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Movian.Api.Settings
{
  public static class ApiConfig
  {
    public static IServiceCollection AddApiConfig(this IServiceCollection services)
    {
      services.AddControllers();

      services.AddApiVersioning(o =>
      {
        o.AssumeDefaultVersionWhenUnspecified = true;
        o.DefaultApiVersion = new ApiVersion(1, 0);
        o.ReportApiVersions = true;
      });


      services.AddVersionedApiExplorer(o =>
      {
        o.GroupNameFormat = "'v'VVV";
        o.SubstituteApiVersionInUrl = true;
      });


      services.Configure<ApiBehaviorOptions>(options =>
      {
        options.SuppressModelStateInvalidFilter = true;
      });

      services.AddCors(options =>
      {
        options.AddPolicy("Development",
                  builder =>
                      builder
                      .AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader());

        options.AddPolicy("Production",
                  builder =>
                      builder
                          .WithMethods("GET")
                          .SetIsOriginAllowedToAllowWildcardSubdomains()
                          // .WithHeaders(HeaderNames.ContentType, "x-custom-header")
                          .AllowAnyHeader());
      });

      return services;
    }

    public static IApplicationBuilder UseApiConfig(this IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseCors("Development");
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseCors("Production"); // Usar apenas nas demos => Configuração Ideal: Production
        app.UseHsts();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseStaticFiles();

      return app;
    }
  }
}