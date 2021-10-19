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
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public CreateUserCommandHandler(ISqlConnectionFactory connectionFactory, IExecutionContextAccessor executionContextAccessor)
    {
      _connectionFactory = connectionFactory;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<CreateUserCommandResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();

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
        await connection.QueryAsync("[membership].[ins_User]", queryParams, commandType: CommandType.StoredProcedure);
      }
      catch (SqlException ex)
      {
        throw new BusinessRuleValidationException(ex.Message);
      }

      return new CreateUserCommandResult();
    }
  }
}
