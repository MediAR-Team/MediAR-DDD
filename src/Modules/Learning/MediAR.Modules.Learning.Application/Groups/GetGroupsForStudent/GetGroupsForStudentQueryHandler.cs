using Dapper;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Learning.Application.Configuration.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Groups.GetGroupsForStudent
{
  class GetGroupsForStudentQueryHandler : IQueryHandler<GetGroupsForStudentQuery, List<GroupDto>>
  {
    private readonly ISqlFacade _sqlFacade;

    public GetGroupsForStudentQueryHandler(ISqlFacade sqlFacade)
    {
      _sqlFacade = sqlFacade;
    }

    public async Task<List<GroupDto>> Handle(GetGroupsForStudentQuery request, CancellationToken cancellationToken)
    {
      const string sql = @"SELECT
                          [GroupMember].[GroupId] AS [Id],
                          [GroupMember].[GroupName] AS [Name],
                          [GroupMember].[TenantId] AS [TenantId]
                          FROM [learning].[v_GroupMembers] [GroupMember]
                          WHERE [UserName] = @Identifier OR [Email] = @Identifier";

      var queryParams = new
      {
        Identifier = request.UserIdentifier
      };

      var groups = await _sqlFacade.QueryAsync<GroupDto>(sql, queryParams);

      return groups.ToList();
    }
  }
}
