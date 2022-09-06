namespace Movian.Api.Identity
{
  public class AuthSettings
  {
    public string Secret { get; set; }
    public int ExpirationTime { get; set; }
    public string Issuer { get; set; }
    public string ValidThrough { get; set; }
  }
}