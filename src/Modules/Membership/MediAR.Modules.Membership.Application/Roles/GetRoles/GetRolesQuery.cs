using MediAR.Modules.Membership.Application.Contracts;
using System.Collections.Generic;

namespace MediAR.Modules.Membership.Application.Roles.GetRoles
{
  public class GetRolesQuery : QueryBase<List<RoleDto>>
  {
    public int Page { get; }
    public int PageSize { get; }

    public GetRolesQuery(int page, int pageSize)
    {
      Page = page;
      PageSize = pageSize;
    }
  }
}
