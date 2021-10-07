using Dapper;
using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Membership.Application.Configuration.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Application.Authorization.GetAuthenticatedUserPermissions
{
  class GetAuthenticatedUserPermissionsQueryHandler : IQueryHandler<GetAuthenticatedUserPermissionsQuery, List<PermissionDto>>
  {
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public GetAuthenticatedUserPermissionsQueryHandler(ISqlConnectionFactory connectionFactory, IExecutionContextAccessor executionContextAccessor)
    {
      _connectionFactory = connectionFactory;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<List<PermissionDto>> Handle(GetAuthenticatedUserPermissionsQuery request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();

      var userId = _executionContextAccessor.UserId;

      const string sql = @"SELECT
                          [Permission].[PermissionId] AS [Id],
                          [Permission].[PermissionName] AS [Name]
                          FROM [membership].[v_UserPermissions] [Permission]
                          WHERE UserId = @UserId";

      var permissions = await connection.QueryAsync<PermissionDto>(sql, new { UserId = userId });

      return permissions.ToList();
    }
  }
}
