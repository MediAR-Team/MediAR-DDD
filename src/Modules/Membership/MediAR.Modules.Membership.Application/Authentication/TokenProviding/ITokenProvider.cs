using MediAR.Modules.Membership.Application.Authentication.Authenticate;

namespace MediAR.Modules.Membership.Application.Authentication.TokenProviding
{
  interface ITokenProvider
  {
    string GenerateToken(UserDto user);
  }
}
