using Dapper;
using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Learning.Application.Configuration.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Groups.GetGroupsForAuthenticatedUser
{
  class GetGroupsForAuthenticatedUserQueryHandler : IQueryHandler<GetGroupsForAuthenticatedUserQuery, List<GroupDto>>
  {
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public GetGroupsForAuthenticatedUserQueryHandler(ISqlConnectionFactory connectionFactory, IExecutionContextAccessor executionContextAccessor)
    {
      _connectionFactory = connectionFactory;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<List<GroupDto>> Handle(GetGroupsForAuthenticatedUserQuery request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();

      const string sql = @"SELECT
                          [GroupMember].[GroupId] AS [Id],
                          [GroupMember].[GroupName] AS [Name],
                          [GroupMember].[TenantId] AS [TenantId]
                          FROM [learning].[v_GroupMembers] [GroupMember]
                          WHERE [UserId] = @UserId";

      var queryParams = new
      {
        _executionContextAccessor.UserId
      };

      var groups = await connection.QueryAsync<GroupDto>(sql, queryParams);

      return groups.ToList();
    }
  }
}
