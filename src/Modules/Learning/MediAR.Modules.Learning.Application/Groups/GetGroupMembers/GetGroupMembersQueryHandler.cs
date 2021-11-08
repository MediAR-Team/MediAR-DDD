using Dapper;
using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Application.Tenants;
using MediAR.Modules.Learning.Application.Configuration.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Groups.GetGroupMembers
{
  class GetGroupMembersQueryHandler : IQueryHandler<GetGroupMembersQuery, List<GroupMemberDto>>
  {
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IExecutionContextAccessor _executionContextAccessor;
    private readonly TenantConfiguration _tenantConfig;

    public GetGroupMembersQueryHandler(ISqlConnectionFactory connectionFactory, IExecutionContextAccessor executionContextAccessor, TenantConfiguration tenantConfig)
    {
      _connectionFactory = connectionFactory;
      _executionContextAccessor = executionContextAccessor;
      _tenantConfig = tenantConfig;
    }

    public async Task<List<GroupMemberDto>> Handle(GetGroupMembersQuery request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();

      const string sql = @"SELECT
                [Member].[UserName] AS [UserName],
                [Member].[Email] AS [Email],
                [Member].[FirstName] AS [FirstName],
                [Member].[LastName] AS [LastName]
                FROM [learning].[v_GroupMembers] [Member]
                WHERE @TenantId IS NULL OR [TenantId] = @TenantId
                AND [GroupId] = @GroupId";

      var queryParams = new
      {
        TenantId = request.TenantId ?? _executionContextAccessor.TenantId,
        request.GroupId
      };

      var result = await connection.QueryAsync<GroupMemberDto>(sql, queryParams);

      return result.ToList();
    }
  }
}
