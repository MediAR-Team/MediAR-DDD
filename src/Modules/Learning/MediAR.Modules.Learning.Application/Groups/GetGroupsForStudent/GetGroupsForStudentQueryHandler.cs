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
    private readonly ISqlConnectionFactory _connectionFactory;

    public GetGroupsForStudentQueryHandler(ISqlConnectionFactory connectionFactory)
    {
      _connectionFactory = connectionFactory;
    }

    public async Task<List<GroupDto>> Handle(GetGroupsForStudentQuery request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();

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

      var groups = await connection.QueryAsync<GroupDto>(sql, queryParams);

      return groups.ToList();
    }
  }
}
