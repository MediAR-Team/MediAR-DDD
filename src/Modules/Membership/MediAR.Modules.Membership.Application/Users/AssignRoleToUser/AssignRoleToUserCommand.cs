using MediAR.Modules.Membership.Application.Contracts;

namespace MediAR.Modules.Membership.Application.Users.AssignRoleToUser
{
  class AssignRoleToUserCommand : CommandBase<AssignRoleToUserCommandResult>
  {
    public string UserIdentifier { get; }
    public UserIdentifierOption IdentifierOption { get; }
    public string RoleName { get; set; }
  }
}
