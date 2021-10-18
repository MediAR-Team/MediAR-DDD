using System;
using System.Collections.Generic;

namespace MediAR.Modules.Membership.Application.Roles
{
  public class RoleDto
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();

    public class PermissionDto
    {
      public string Name { get; set; }
      public string Description { get; set; }
    }
  }
}
