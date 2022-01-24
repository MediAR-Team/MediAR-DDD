using Dapper;
using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Application.Queries;
using MediAR.Modules.Learning.Application.Configuration.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Groups.GetGroups
{
  class GetGroupsQueryHandler : IQueryHandler<GetGroupsQuery, List<GroupDto>>
  {
    private readonly ISqlFacade _sqlFacade;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public GetGroupsQueryHandler(ISqlFacade sqlFacade, IExecutionContextAccessor executionContextAccessor)
    {
      _sqlFacade = sqlFacade;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<List<GroupDto>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
    {
      var requestParams = new DynamicParameters();
      var pageData = PagedQueryHelper.GetPageData(request);
      requestParams.Add(PagedQueryHelper.Next, pageData.Next);
      requestParams.Add(PagedQueryHelper.Offset, pageData.Offset);

      var sql = @"SELECT
                  [Group].[Id] AS [Id],
                  [Group].[Name] AS [Name],
                  [Group].[TenantId] AS [TenantId]
                  FROM [learning].[v_Groups] [Group]
                  WHERE [TenantId] = @TenantId";

      requestParams.Add("TenantId", _executionContextAccessor.TenantId);

      sql += "\nORDER BY [Id]";

      sql = PagedQueryHelper.AppendPageStatement(sql);

      var result = await _sqlFacade.QueryAsync<GroupDto>(sql, requestParams);

      return result.ToList();
    }
  }
}
