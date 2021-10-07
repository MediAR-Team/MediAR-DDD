using MediAR.Modules.Membership.Application.Contracts;

namespace MediAR.Modules.Membership.Application.Authentication.Authenticate
{
  public class AuthenticateCommand : CommandBase<AuthenticationResult>
  {
    public string UserName { get; }
    public string Password { get; }

    public AuthenticateCommand(string userName, string password)
    {
      UserName = userName;
      Password = password;
    }
  }
}
