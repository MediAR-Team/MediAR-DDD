using Dapper;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.TenantManagement.Application.Configuration.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.TenantManagement.Application.Tenants.GetTenantByRefer
{
  class GetTenantByReferQueryHandler : IQueryHandler<GetTenantByReferQuery, TenantDto>
  {
    private readonly ISqlConnectionFactory _connectionFactory;

    public GetTenantByReferQueryHandler(ISqlConnectionFactory connectionFactory)
    {
      _connectionFactory = connectionFactory;
    }

    public async Task<TenantDto> Handle(GetTenantByReferQuery request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();
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

      var result = await connection.QuerySingleAsync<TenantDto>(sql, queryParams);

      return result;
    }
  }
}
