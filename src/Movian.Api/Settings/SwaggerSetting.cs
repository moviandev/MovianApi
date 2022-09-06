using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Movian.Api.Settings
{
  public static class SwaggerConfigurations
  {
    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
      services.AddSwaggerGen(o =>
      {
        o.OperationFilter<SwaggerDefaultValues>();

        o.AddSecurityDefinition(
            "Bearer",
            new OpenApiSecurityScheme
            {
              Description = "",
              Name = "Authorization",
              In = ParameterLocation.Header,
              Type = SecuritySchemeType.ApiKey,
            });

        var securityRequirement = new OpenApiSecurityRequirement
        {
          {
            new OpenApiSecurityScheme
            {
              Reference = new OpenApiReference
              {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
              }
            },
            new string[]{}
          }
        };

        o.AddSecurityRequirement(securityRequirement);
      });

      return services;
    }

    public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
    {
      app.UseSwagger();
      app.UseSwaggerUI(o =>
      {
        foreach (var description in provider.ApiVersionDescriptions)
          o.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
      });

      return app;
    }
  }

  public class SwaggerConfig : IConfigureOptions<SwaggerGenOptions>
  {
    readonly IApiVersionDescriptionProvider provider;

    public SwaggerConfig(IApiVersionDescriptionProvider provider)
    {
      this.provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
      foreach (var description in provider.ApiVersionDescriptions)
        options.SwaggerDoc(description.GroupName, CreateInoForApiVersion(description));
    }

    static OpenApiInfo CreateInoForApiVersion(ApiVersionDescription description)
    {
      var info = new OpenApiInfo()
      {
        Title = "API - Developed along the Desenvolvedor.io course",
        Version = description.ApiVersion.ToString(),
        Description = "This app was developed along side Eduardo Pires Course",
        Contact = new OpenApiContact() { Name = "Matheus Viana", Email = "teste@teste.com" },
        TermsOfService = new Uri("https://opensource.org/licenses/MIT"),
        License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") },
      };

      if (description.IsDeprecated)
        info.Description += "This version is deprecated";

      return info;
    }
  }

  public class SwaggerDefaultValues : IOperationFilter
  {
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
      var apiDescription = context.ApiDescription;

      operation.Deprecated = apiDescription.IsDeprecated();

      if (operation.Parameters is null)
        return;

      foreach (var parameter in operation.Parameters.OfType<OpenApiParameter>())
      {
        var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

        if (parameter.Description == null)
          parameter.Description = description.ModelMetadata?.Description;

        parameter.Required |= description.IsRequired;
      }
    }
  }
}