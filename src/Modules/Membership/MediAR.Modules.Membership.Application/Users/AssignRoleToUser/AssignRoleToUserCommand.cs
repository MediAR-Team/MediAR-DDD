using MediAR.Modules.Membership.Application.Contracts;

namespace MediAR.Modules.Membership.Application.Users.AssignRoleToUser
{
  public class AssignRoleToUserCommand : CommandBase<AssignRoleToUserCommandResult>
  {
    public string UserIdentifier { get; }
    public UserIdentifierOption IdentifierOption { get; }
    public string RoleName { get; }

    public AssignRoleToUserCommand(UserIdentifierOption identifierOption, string identifier, string roleName)
    {
      IdentifierOption = identifierOption;
      UserIdentifier = identifier;
      RoleName = roleName;
    }
  }
}
