namespace MediAR.Modules.Membership.Application.Authentication.Authenticate
{
  public class AuthenticationResult
  {
    public bool IsSuccessful { get; }
    public string AccessToken { get; }
    // TODO: implement refresh tokens
    public string RefreshToken { get; }

    public string Error { get; }

    public AuthenticationResult(string error)
    {
      IsSuccessful = false;
      Error = error;
    }

    public AuthenticationResult(string accessToken, string refreshToken)
    {
      IsSuccessful = true;
      AccessToken = accessToken;
      RefreshToken = refreshToken;
    }
  }
}
