using MediAR.Modules.Membership.Application.Contracts;

namespace MediAR.Modules.Membership.Application.Users.AssignRoleToUser
{
  class PublishUserAddedToRoleIntegrationEventCommand : CommandBase
  {
    public string UserNameOrEmail { get; }
    public string RoleName { get; }

    public PublishUserAddedToRoleIntegrationEventCommand(string userNameOrEmail, string roleName)
    {
      UserNameOrEmail = userNameOrEmail;
      RoleName = roleName;
    }
  }
}
