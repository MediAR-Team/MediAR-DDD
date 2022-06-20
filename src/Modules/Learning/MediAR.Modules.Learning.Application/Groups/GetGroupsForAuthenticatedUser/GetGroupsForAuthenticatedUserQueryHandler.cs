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
    private readonly ISqlFacade _sqlFacade;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public GetGroupsForAuthenticatedUserQueryHandler(ISqlFacade sqlFacade, IExecutionContextAccessor executionContextAccessor)
    {
      _sqlFacade = sqlFacade;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<List<GroupDto>> Handle(GetGroupsForAuthenticatedUserQuery request, CancellationToken cancellationToken)
    {
      const string sql = @"SELECT
                            [GroupMember].[GroupId] AS [Id],
                            [GroupMember].[GroupName] AS [Name],
                            [GroupMember].[TenantId] AS [TenantId]
                          FROM [learning].[v_GroupMembers] [GroupMember]
                          WHERE [StudentId] = @UserId";

      var queryParams = new
      {
        _executionContextAccessor.UserId
      };

      var groups = await _sqlFacade.QueryAsync<GroupDto>(sql, queryParams);

      return groups.ToList();
    }
  }
}
