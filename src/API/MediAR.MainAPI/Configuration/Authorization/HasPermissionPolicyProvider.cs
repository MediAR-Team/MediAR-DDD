using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace MediAR.MainAPI.Configuration.Authorization
{
  internal class HasPermissionPolicyProvider : IAuthorizationPolicyProvider
  {
    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
    {
      return Task.FromResult<AuthorizationPolicy>(null);
    }

    public Task<AuthorizationPolicy> GetFallbackPolicyAsync()
    {
      return Task.FromResult<AuthorizationPolicy>(null);
    }

    public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    {
      if (policyName.StartsWith(HasPermissionAttribute.HasPermissionPolicyName, StringComparison.OrdinalIgnoreCase))
      {
        var policy = new AuthorizationPolicyBuilder();
        policy.AddRequirements(new HasPermissionAuthorizationRequirement(policyName.Substring(HasPermissionAttribute.HasPermissionPolicyName.Length)));
        policy.AddAuthenticationSchemes("Bearer");

        return Task.FromResult(policy.Build());
      }

      return Task.FromResult<AuthorizationPolicy>(null);
    }
  }
}
