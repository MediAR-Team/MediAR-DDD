using MediAR.Modules.Membership.Application.Contracts;
using System;
using System.Collections.Generic;

namespace MediAR.Modules.Membership.Application.Authorization.GetUserPermissions
{
  public class GetUserPermissionsQuery : QueryBase<List<PermissionDto>>
  {
    public Guid UserId { get; }

    public GetUserPermissionsQuery(Guid userId)
    {
      UserId = userId;
    }
  }
}
