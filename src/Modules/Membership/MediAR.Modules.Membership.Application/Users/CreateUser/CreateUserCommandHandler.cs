using Dapper;
using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Domain;
using MediAR.Modules.Membership.Application.Configuration.Commands;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Application.Users.CreateUser
{
  class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CreateUserCommandResult>
  {
    private readonly ISqlFacade _sqlFacade;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public CreateUserCommandHandler(ISqlFacade sqlFacade, IExecutionContextAccessor executionContextAccessor)
    {
      _sqlFacade = sqlFacade;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<CreateUserCommandResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
      var passwordHash = PasswordManager.HashPassword(request.Password);

      var queryParams = new
      {
        request.UserName,
        request.Email,
        PasswordHash = passwordHash,
        request.FirstName,
        request.LastName,
        _executionContextAccessor.TenantId
      };

      try
      {
        await _sqlFacade.ExecuteAsync("[membership].[ins_User]", queryParams, commandType: CommandType.StoredProcedure);
      }
      catch (SqlException ex)
      {
        throw new BusinessRuleValidationException(ex.Message);
      }

      return new CreateUserCommandResult();
    }
  }
}
