using Dapper;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Application.Tenants;
using MediAR.Coreplatform.Domain;
using MediAR.Modules.Membership.Application.Configuration.Commands;
using MediAR.Modules.Membership.Domain.Users;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Application.Users.RegisterUser
{
  class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, RegisterUserCommandResult>
  {
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IUsersCounter _usersCounter;
    private readonly TenantConfiguration _tenantConfig;

    public RegisterUserCommandHandler(ISqlConnectionFactory connectionFactory, IUsersCounter usersCounter, TenantConfiguration tenantConfig)
    {
      _connectionFactory = connectionFactory;
      _usersCounter = usersCounter;
      _tenantConfig = tenantConfig;
    }

    public async Task<RegisterUserCommandResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();

      var usersCount = await _usersCounter.CountUsersWithUserNameOrEmailAsync(request.UserName, request.Email);

      if (usersCount > 0)
      {
        throw new BusinessRuleValidationException("UserName and Email must be unique");
      }

      var passwordHash = PasswordManager.HashPassword(request.Password);

      var queryParams = new
      {
        request.UserName,
        request.Email,
        PasswordHash = passwordHash,
        request.FirstName,
        request.LastName,
        // TODO: fix
        TenantId = _tenantConfig.DefaultTenantId
      };

      await connection.QueryAsync("[membership].[ins_User]", queryParams, commandType: CommandType.StoredProcedure);

      // TODO: maybe return auth response or something
      return new RegisterUserCommandResult();
    }
  }
}
