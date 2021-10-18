using MediAR.Modules.Membership.Application.Authentication.Authenticate;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MediAR.Modules.Membership.Application.Authentication.TokenProviding
{
  public class TokenProvider : ITokenProvider
  {
    private readonly TokenConfiguration _config;

    public TokenProvider(TokenConfiguration config)
    {
      _config = config;
    }

    public string GenerateToken(UserDto user)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.UTF8.GetBytes(_config.JwtSecret);
      var claims = new List<Claim>
        {
          new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
          new("tenantId", user.TenantId.ToString()),
          new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

      // TODO: write role information to token

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.Add(_config.ClaimsTokenExp),
        SigningCredentials =
              new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
        Audience = _config.JwtAudience,
        Issuer = _config.JwtIssuer
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}
