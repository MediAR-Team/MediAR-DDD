using Dapper;
using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Domain;
using MediAR.Modules.Learning.Application.Configuration.Commands;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Modules.CreateModule
{
  class CreateModuleCommandHandler : ICommandHandler<CreateModuleCommand, ModuleDto>
  {
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public CreateModuleCommandHandler(ISqlConnectionFactory connectionFactory, IExecutionContextAccessor executionContextAccessor)
    {
      _connectionFactory = connectionFactory;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<ModuleDto> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();

      var queryParams = new
      {
        request.Name,
        request.TenantId,
        request.CourseId
      };

      try
      {
        var result = await connection.ExecuteScalarAsync<ModuleDto>("[learning].[ins_Module]", queryParams, commandType: CommandType.StoredProcedure);
        return result;
      }
      catch (SqlException ex)
      {
        throw new BusinessRuleValidationException(ex.Message);
      }
    }
  }
}
