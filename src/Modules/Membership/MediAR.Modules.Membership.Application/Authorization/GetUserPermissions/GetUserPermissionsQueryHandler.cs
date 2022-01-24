using Dapper;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Membership.Application.Configuration.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Application.Authorization.GetUserPermissions
{
  class GetUserPermissionsQueryHandler : IQueryHandler<GetUserPermissionsQuery, List<PermissionDto>>
  {
    private readonly ISqlFacade _sqlFacade;

    public GetUserPermissionsQueryHandler(ISqlFacade sqlFacade)
    {
      _sqlFacade = sqlFacade;
    }

    public async Task<List<PermissionDto>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
    {
      const string sql = @"SELECT
                          [Permission].[PermissionId] AS [Id],
                          [Permission].[PermissionName] AS [Name]
                          FROM [membership].[v_UserPermissions] [Permission]
                          WHERE UserId = @UserId";

      var permissions = await _sqlFacade.QueryAsync<PermissionDto>(sql, new { UserId = request.UserId });

      return permissions.ToList();
    }
  }
}
