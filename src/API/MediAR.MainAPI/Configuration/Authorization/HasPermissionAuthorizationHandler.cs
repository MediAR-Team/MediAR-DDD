using MediAR.Modules.Membership.Application.Authorization.GetAuthenticatedUserPermissions;
using MediAR.Modules.Membership.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace MediAR.MainAPI.Configuration.Authorization
{
  internal class HasPermissionAuthorizationHandler : AttributeAuthorizationHandler<HasPermissionAuthorizationRequirement, HasPermissionAttribute>
  {
    private readonly IMembershipModule _membershipModule;

    public HasPermissionAuthorizationHandler(IMembershipModule membershipModule)
    {
      _membershipModule = membershipModule;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, HasPermissionAuthorizationRequirement requirement, HasPermissionAttribute attribute)
    {
      var permissions = await _membershipModule.ExecuteQueryAsync(new GetAuthenticatedUserPermissionsQuery());

      if (!permissions.Any(x => x.Name == attribute.Name))
      {
        context.Fail();
        return;
      }

      context.Succeed(requirement);
    }
  }
}
