using Dapper;
using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Application.Queries;
using MediAR.Coreplatform.Application.Tenants;
using MediAR.Modules.Learning.Application.Configuration.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Groups.GetGroups
{
  class GetGroupsQueryHandler : IQueryHandler<GetGroupsQuery, List<GroupDto>>
  {
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IExecutionContextAccessor _executionContextAccessor;
    private readonly TenantConfiguration _tenantConfig;

    public GetGroupsQueryHandler(ISqlConnectionFactory connectionFactory, IExecutionContextAccessor executionContextAccessor, TenantConfiguration tenantConfig)
    {
      _connectionFactory = connectionFactory;
      _executionContextAccessor = executionContextAccessor;
      _tenantConfig = tenantConfig;
    }

    public async Task<List<GroupDto>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();

      var requestParams = new DynamicParameters();
      var pageData = PagedQueryHelper.GetPageData(request);
      requestParams.Add(PagedQueryHelper.Next, pageData.Next);
      requestParams.Add(PagedQueryHelper.Offset, pageData.Offset);

      var sql = @"SELECT
                  [Group].[Id] AS [Id],
                  [Group].[Name] AS [Name]
                  [Group].[TenantId] AS [TenantId]
                  FROM [learning].[v_Groups] [Group]";

      var isMasterTenant = _executionContextAccessor.TenantId == _tenantConfig.MasterTenantId;
      if (!isMasterTenant)
      {
        sql += "WHERE [TenantId] = @TenantId";
        requestParams.Add("TenantId", _executionContextAccessor.TenantId);
      }

      sql += "ORDER BY [Id]";

      sql = PagedQueryHelper.AppendPageStatement(sql);

      var result = await connection.QueryAsync<GroupDto>(sql, requestParams);

      return result.ToList();
    }
  }
}
