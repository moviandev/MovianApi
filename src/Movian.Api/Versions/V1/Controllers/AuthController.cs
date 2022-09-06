using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Movian.Api.Controllers;
using Movian.Api.Identity;
using Movian.Api.Models;
using Movian.Business.Interfaces;

namespace Movian.Api.Versions.V1.Controllers
{
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/")]
  public class AuthController : BaseController
  {
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AuthSettings _authSettings;

    public AuthController(INotifier notifier,
                          SignInManager<IdentityUser> signInManager,
                          UserManager<IdentityUser> userManager,
                          IOptions<AuthSettings> authSettings) : base(notifier)
    {
      _signInManager = signInManager;
      _userManager = userManager;
      _authSettings = authSettings.Value;
    }

    [HttpPost("sign-in")]
    public async Task<ActionResult> SignIn(SignInDto signIn)
    {
      if (!ModelState.IsValid)
        return CustomResponse(ModelState);

      var user = new IdentityUser
      {
        UserName = signIn.Email,
        Email = signIn.Email,
        EmailConfirmed = true
      };

      var result = await _userManager.CreateAsync(user);

      if (result.Succeeded)
      {
        await _signInManager.SignInAsync(user, false);
        return CustomResponse(await GenerateJwt(signIn.Email));
      }

      foreach (var error in result.Errors)
        NotifyError(error.Description);

      return CustomResponse();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginDto login)
    {
      if (!ModelState.IsValid)
        return CustomResponse(ModelState);

      var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, true);

      if (result.Succeeded)
      {
        return CustomResponse(await GenerateJwt(login.Email));
      }

      if (result.IsLockedOut)
      {
        NotifyError("User exceeded the maximum tentatives, try again later");
        return CustomResponse();
      }

      NotifyError("User or password is invalid");
      return CustomResponse();
    }



    private async Task<LoginResponseDto> GenerateJwt(string email)
    {
      var user = await _userManager.FindByEmailAsync(email);
      var claims = await _userManager.GetClaimsAsync(user);
      var userRoles = await _userManager.GetRolesAsync(user);

      claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
      claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
      claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
      claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
      claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

      foreach (var role in userRoles)
        claims.Add(new Claim("role", role));

      var identityClaims = new ClaimsIdentity();
      identityClaims.AddClaims(claims);

      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_authSettings.Secret);
      var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
      {
        Issuer = _authSettings.Issuer,
        Audience = _authSettings.ValidThrough,
        Expires = DateTime.UtcNow.AddHours(_authSettings.ExpirationTime),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        Subject = identityClaims
      });

      var encodedToken = tokenHandler.WriteToken(token);

      var response = new LoginResponseDto
      {
        AccessToken = encodedToken,
        ExpiresIn = TimeSpan.FromHours(_authSettings.ExpirationTime).TotalSeconds,
        UserToken = new UserTokenDto
        {
          Id = user.Id,
          Email = user.Email,
          Claims = claims.Select(c => new ClaimDto { Type = c.Type, Value = c.Value })
        }
      };

      return response;
    }

    private static long ToUnixEpochDate(DateTime date)
      => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
  }
}