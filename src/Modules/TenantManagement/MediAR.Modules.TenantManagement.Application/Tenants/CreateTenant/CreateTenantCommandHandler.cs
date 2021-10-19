using Dapper;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Domain;
using MediAR.Modules.TenantManagement.Application.Configuration.Commands;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.TenantManagement.Application.Tenants.CreateTenant
{
  class CreateTenantCommandHandler : ICommandHandler<CreateTenantCommand, TenantDto>
  {
    private readonly ISqlConnectionFactory _connectionFactory;

    public CreateTenantCommandHandler(ISqlConnectionFactory connectionFactory)
    {
      _connectionFactory = connectionFactory;
    }

    public async Task<TenantDto> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();

      var requestParams = new
      {
        request.Name,
        request.ConnectionString
      };

      try
      {
        var result = await connection.QueryFirstOrDefaultAsync<TenantDto>("[tenants].[ins_Tenant]", requestParams, commandType: CommandType.StoredProcedure);
        return result;
      }
      catch (SqlException ex)
      {
        throw new BusinessRuleValidationException(ex.Message);
      }

    }
  }
}
