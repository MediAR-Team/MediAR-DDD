using Dapper;
using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Domain;
using MediAR.Modules.Membership.Application.Configuration.Commands;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Application.Users.RegisterUser
{
  class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, RegisterUserCommandResult>
  {
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public RegisterUserCommandHandler(ISqlConnectionFactory connectionFactory, IExecutionContextAccessor executionContextAccessor)
    {
      _connectionFactory = connectionFactory;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<RegisterUserCommandResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
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
        await connection.ExecuteAsync("[membership].[ins_User]", queryParams, commandType: CommandType.StoredProcedure);
      }
      catch (SqlException ex)
      {
        throw new BusinessRuleValidationException(ex.Message);
      }

      // TODO: maybe return auth response or something
      return new RegisterUserCommandResult();
    }
  }
}
