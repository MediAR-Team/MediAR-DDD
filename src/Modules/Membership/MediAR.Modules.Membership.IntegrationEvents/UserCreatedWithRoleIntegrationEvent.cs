using MediAR.Coreplatform.Infrastructure.EventBus;
using System;

namespace MediAR.Modules.Membership.IntegrationEvents
{
  public class UserCreatedWithRoleIntegrationEvent : IntegrationEvent
  {
    public UserCreatedWithRoleIntegrationEvent(Guid id, DateTime occuredOn, Guid userId, string email, string userName, string firstName, string lastName, string roleName, Guid tenantId) : base(id, occuredOn)
    {
      UserId = userId;
      Email = email;
      UserName = userName;
      FirstName = firstName;
      LastName = lastName;
      RoleName = roleName;
      TenantId = tenantId;
    }

    public Guid UserId { get; }
    public string Email { get; }
    public string UserName { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string RoleName { get; }
    public Guid TenantId { get; }

  }
}
