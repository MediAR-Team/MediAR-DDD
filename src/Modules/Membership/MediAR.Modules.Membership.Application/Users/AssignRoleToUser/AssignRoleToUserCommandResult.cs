using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Application.Users.AssignRoleToUser
{
  public class AssignRoleToUserCommandResult
  {
    public bool IsSuccessful { get; }
    public string Error { get; }

    public AssignRoleToUserCommandResult()
    {
      IsSuccessful = true;
    }

    public AssignRoleToUserCommandResult(string error)
    {
      IsSuccessful = false;
      Error = error;
    }
  }
}
