using Dapper;
using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Domain;
using MediAR.Modules.Learning.Application.Configuration.Commands;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Groups.CreateGroup
{
  class CreateGroupCommandHandler : ICommandHandler<CreateGroupCommand, GroupDto>
  {
    private readonly ISqlFacade _sqlFacade;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public CreateGroupCommandHandler(ISqlFacade sqlFacade, IExecutionContextAccessor executionContextAccessor)
    {
      _sqlFacade = sqlFacade;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<GroupDto> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
      var queryParams = new
      {
        request.Name,
        TenantId = request.TenantId ?? _executionContextAccessor.TenantId
      };

      try
      {
        var result = await _sqlFacade.QueryFirstOrDefaultAsync<GroupDto>("[learning].[ins_Group]", queryParams, commandType: CommandType.StoredProcedure);
        return result;
      }
      catch (SqlException ex)
      {
        throw new BusinessRuleValidationException(ex.Message);
      }
    }
  }
}
