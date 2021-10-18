﻿using Dapper;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Application.Tenants;
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
    private readonly TenantConfiguration _tenantConfig;

    public RegisterUserCommandHandler(ISqlConnectionFactory connectionFactory, TenantConfiguration tenantConfig)
    {
      _connectionFactory = connectionFactory;
      _tenantConfig = tenantConfig;
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
        // TODO: fix
        TenantId = _tenantConfig.DefaultTenantId
      };

      try
      {
        await connection.QueryAsync("[membership].[ins_User]", queryParams, commandType: CommandType.StoredProcedure);
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
