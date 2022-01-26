using MediAR.Modules.Membership.Application.Contracts;
using System;

namespace MediAR.Modules.Membership.Application.Users
{
  internal class PublishUserWithRoleCreatedIntegrationEvent : CommandBase
  {
    public Guid UserId { get; }

    public PublishUserWithRoleCreatedIntegrationEvent(Guid userId, Guid tenantId, string userName, string email, string firstName, string lastName, string roleName)
    {
      UserId = userId;
      TenantId = tenantId;
      UserName = userName;
      Email = email;
      FirstName = firstName;
      LastName = lastName;
      RoleName = roleName;
    }

    public Guid TenantId { get; }
    public string UserName { get; }
    public string Email { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string RoleName { get; }
  }
}
