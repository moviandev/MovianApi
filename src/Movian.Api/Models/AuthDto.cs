using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movian.Api.Models
{
  public class SignInDto
  {
    [Required(ErrorMessage = "The field {0} is required")]
    [EmailAddress(ErrorMessage = "The {0} is not valid")]
    public string Email { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(100, ErrorMessage = "The field {0} needs to be in between {2} e {1} characteres", MinimumLength = 6)]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "The passwords are not the same")]
    public string ConfirmPassword { get; set; }
  }

  public class LoginDto
  {
    [Required(ErrorMessage = "The field {0} is required")]
    [EmailAddress(ErrorMessage = "The {0} is not valid")]
    public string Email { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(100, ErrorMessage = "The field {0} needs to be in between {2} e {1} characteres", MinimumLength = 6)]
    public string Password { get; set; }
  }

  public class UserTokenDto
  {
    public string Id { get; set; }
    public string Email { get; set; }
    public IEnumerable<ClaimDto> Claims { get; set; }
  }

  public class LoginResponseDto
  {
    public string AccessToken { get; set; }
    public double ExpiresIn { get; set; }
    public UserTokenDto UserToken { get; set; }
  }

  public class ClaimDto
  {
    public string Value { get; set; }
    public string Type { get; set; }
  }

}