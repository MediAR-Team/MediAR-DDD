using Microsoft.AspNetCore.Authorization;

namespace MediAR.MainAPI.Configuration.Authorization
{
  public class HasPermissionAuthorizationRequirement : IAuthorizationRequirement
  {
    public string Name { get; }

    public HasPermissionAuthorizationRequirement(string name)
    {
      Name = name;
    }
  }
}
