using MediAR.Modules.Membership.Application.Users;
using System.ComponentModel.DataAnnotations;

namespace MediAR.MainAPI.Modules.Membership.Users
{
  public class AssignRoleToUserRequest
  {
    [Required]
    public string UserIdentifier { get; set; }
    [Required]
    public UserIdentifierOption IdentifierOption { get; set; }
    [Required]
    public string RoleName { get; set; }
  }
}
