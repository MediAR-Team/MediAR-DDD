using MediAR.Modules.Membership.Application.Authorization.GetAuthenticatedUserPermissions;
using MediAR.Modules.Membership.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MediAR.MainAPI.Configuration.Authorization
{
  internal class HasPermissionAuthorizationHandler : AuthorizationHandler<HasPermissionAuthorizationRequirement>
  {
    private readonly IMembershipModule _membershipModule;

    public HasPermissionAuthorizationHandler(IMembershipModule membershipModule)
    {
      _membershipModule = membershipModule;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, HasPermissionAuthorizationRequirement requirement)
    {
      var permissions = await _membershipModule.ExecuteQueryAsync(new GetAuthenticatedUserPermissionsQuery());

      if (!permissions.Any(x => x.Name.Equals(requirement.Name, StringComparison.OrdinalIgnoreCase)))
      {
        context.Fail();
        return;
      }

      context.Succeed(requirement);
    }
  }
}
