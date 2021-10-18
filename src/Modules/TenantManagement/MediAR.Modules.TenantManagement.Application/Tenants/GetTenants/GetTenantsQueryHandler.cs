using Dapper;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Application.Queries;
using MediAR.Modules.TenantManagement.Application.Configuration.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.TenantManagement.Application.Tenants.GetTenants
{
  class GetTenantsQueryHandler : IQueryHandler<GetTenantsQuery, List<TenantDto>>
  {
    private readonly ISqlConnectionFactory _connectionFactory;

    public GetTenantsQueryHandler(ISqlConnectionFactory connectionFactory)
    {
      _connectionFactory = connectionFactory;
    }

    public async Task<List<TenantDto>> Handle(GetTenantsQuery request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();
      var pageData = PagedQueryHelper.GetPageData(request);
      var queryParams = new DynamicParameters();
      queryParams.Add(PagedQueryHelper.Next, pageData.Next);
      queryParams.Add(PagedQueryHelper.Offset, pageData.Offset);

      const string sql = @"SELECT
                          [Tenant].[Id] AS [Id],
                          [Tenant].[Name] AS [Name],
                          [Tenant].[ConnectionString] AS [ConnectionString]
                          ORDER BY [Id]";

      PagedQueryHelper.AppendPageStatement(sql);

      var result = await connection.QueryAsync<TenantDto>(sql, queryParams);

      return result.ToList();
    }
  }
}
