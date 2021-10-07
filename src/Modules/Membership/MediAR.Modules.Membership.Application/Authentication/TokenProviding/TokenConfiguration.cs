using System;

namespace MediAR.Modules.Membership.Application.Authentication.TokenProviding
{
  public class TokenConfiguration
  {
    public TimeSpan ClaimsTokenExp { get; set; }
    public string JwtSecret { get; set; }
    public string JwtIssuer { get; set; }
    public string JwtAudience { get; set; }
  }
}
