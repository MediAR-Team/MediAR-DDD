using Dapper;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Membership.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static MediAR.Modules.Membership.Application.Roles.RoleDto;

namespace MediAR.Modules.Membership.Application.Roles.GetRoles
{
  class GetRolesQueryHandler : IQueryHandler<GetRolesQuery, List<RoleDto>>
  {
    private readonly ISqlConnectionFactory _connectionFactory;

    public GetRolesQueryHandler(ISqlConnectionFactory connectionFactory)
    {
      _connectionFactory = connectionFactory;
    }

    public async Task<List<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();
      var queryParams = new { Offset = request.PageSize * (request.Page - 1), Next = request.PageSize };

      var lookup = new Dictionary<Guid, RoleDto>();
      await connection.QueryAsync<RoleDto, PermissionDto, RoleDto>("[membership].[sel_Roles_with_Permissions]", (r, p) =>
      {
        if (!lookup.TryGetValue(r.Id, out RoleDto role))
        {
          lookup.Add(r.Id, r);
          role = r;
        }

        role.Permissions.Add(p);

        return role;
      }, queryParams, commandType: CommandType.StoredProcedure, splitOn: "Name");

      var result = lookup.Values;

      return result.ToList();
    }
  }
}
