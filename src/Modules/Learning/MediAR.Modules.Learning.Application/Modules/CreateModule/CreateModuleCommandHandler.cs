using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Domain;
using MediAR.Coreplatform.Infrastructure.Data;
using MediAR.Modules.Learning.Application.Configuration.Commands;
using MediatR;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Modules.CreateModule
{
  class CreateModuleCommandHandler : ICommandHandler<CreateModuleCommand>
  {
    private readonly ISqlFacade _sqlFacade;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public CreateModuleCommandHandler(ISqlFacade sqlFacade, IExecutionContextAccessor executionContextAccessor)
    {
      _sqlFacade = sqlFacade;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<Unit> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
    {
      var queryParams = new
      {
        request.Name,
        TenantId = request.TenantId ?? _executionContextAccessor.TenantId,
        request.CourseId
      };

      try
      {
        await _sqlFacade.ExecuteAsync("[learning].[ins_Module]", queryParams, commandType: CommandType.StoredProcedure);
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
