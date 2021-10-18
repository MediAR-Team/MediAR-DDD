using MediAR.Modules.Membership.Application.Authentication.Authenticate;

namespace MediAR.Modules.Membership.Application.Authentication.TokenProviding
{
  public interface ITokenProvider
  {
    string GenerateToken(UserDto user);
  }
}
