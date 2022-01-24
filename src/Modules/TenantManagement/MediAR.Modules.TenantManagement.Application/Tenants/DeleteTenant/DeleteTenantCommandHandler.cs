using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Domain;
using MediAR.Coreplatform.Infrastructure.Data;
using MediAR.Modules.TenantManagement.Application.Configuration.Commands;
using MediatR;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.TenantManagement.Application.Tenants.DeleteTenant
{
  class DeleteTenantCommandHandler : ICommandHandler<DeleteTenantCommand>
  {
    private readonly ISqlFacade _sqlFacade;

    public DeleteTenantCommandHandler(ISqlFacade sqlFacade)
    {
      _sqlFacade = sqlFacade;
    }

    public async Task<Unit> Handle(DeleteTenantCommand request, CancellationToken cancellationToken)
    {
      var queryParams = new
      {
        request.TenantId
      };

      try
      {
        await _sqlFacade.ExecuteAsync("[tenants].[del_Tenant]", queryParams, commandType: CommandType.StoredProcedure);
        return Unit.Value;
      }
      catch (SqlException ex)
      {
        if (ex.Number == SqlConstants.UserDefinedExceptionCode)
        {
          throw new BusinessRuleValidationException(ex.Message);
        }
        throw;
      }
    }
  }
}
