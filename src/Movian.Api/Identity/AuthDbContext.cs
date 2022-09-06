using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Movian.Api.Identity
{
  public class AuthDbContext : IdentityDbContext
  {
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }
  }
}