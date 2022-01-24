using Dapper;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.TenantManagement.Application.Configuration.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.TenantManagement.Application.Tenants.GetTenantByRefer
{
  class GetTenantByReferQueryHandler : IQueryHandler<GetTenantByReferQuery, TenantDto>
  {
    private readonly ISqlFacade _sqlFacade;

    public GetTenantByReferQueryHandler(ISqlFacade sqlFacade)
    {
      _sqlFacade = sqlFacade;
    }

    public async Task<TenantDto> Handle(GetTenantByReferQuery request, CancellationToken cancellationToken)
    {
      var queryParams = new
      {
        request.ReferUrl
      };

      var sql = @"SELECT
                  [Tenant].[Id] AS [Id],
                  [Tenant].[Name] AS [Name],
                  [Tenant].[ConnectionString] AS [ConnectionString]
                  FROM [tenants].[v_Tenants] [Tenant]
                  WHERE [Tenant].[ReferUrl] = @ReferUrl";

      var result = await _sqlFacade.QueryFirstOrDefaultAsync<TenantDto>(sql, queryParams);

      return result;
    }
  }
}
