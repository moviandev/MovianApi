using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Movian.Api.Identity;

namespace Movian.Api.Settings
{
  public static class IdentitySetting
  {
    public static IServiceCollection AddIdentitySetting(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<AuthDbContext>(o =>
        o.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

      services.AddDefaultIdentity<IdentityUser>()
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<AuthDbContext>()
        .AddDefaultTokenProviders();

      var appSettingsSection = configuration.GetSection("AuthSettings");
      services.Configure<AuthSettings>(appSettingsSection);

      var authSettings = appSettingsSection.Get<AuthSettings>();
      var key = Encoding.ASCII.GetBytes(authSettings.Secret);

      services.AddAuthentication(o =>
      {
        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(o =>
      {
        o.RequireHttpsMetadata = true;
        o.SaveToken = true;
        o.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidAudience = authSettings.ValidThrough,
          ValidIssuer = authSettings.Issuer,
        };
      });

      return services;
    }
  }
}