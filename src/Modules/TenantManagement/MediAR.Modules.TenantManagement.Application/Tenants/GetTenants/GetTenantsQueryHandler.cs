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
    private readonly ISqlFacade _sqlFacade;

    public GetTenantsQueryHandler(ISqlFacade sqlFacade)
    {
      _sqlFacade = sqlFacade;
    }

    public async Task<List<TenantDto>> Handle(GetTenantsQuery request, CancellationToken cancellationToken)
    {
      var pageData = PagedQueryHelper.GetPageData(request);
      var queryParams = new DynamicParameters();
      queryParams.Add(PagedQueryHelper.Next, pageData.Next);
      queryParams.Add(PagedQueryHelper.Offset, pageData.Offset);

      var sql = @"SELECT
                          [Tenant].[Id] AS [Id],
                          [Tenant].[Name] AS [Name],
                          [Tenant].[ConnectionString] AS [ConnectionString]
                          FROM [tenants].[v_Tenants] [Tenant]
                          ORDER BY [Id]";

      sql = PagedQueryHelper.AppendPageStatement(sql);

      var result = await _sqlFacade.QueryAsync<TenantDto>(sql, queryParams);

      return result.ToList();
    }
  }
}
