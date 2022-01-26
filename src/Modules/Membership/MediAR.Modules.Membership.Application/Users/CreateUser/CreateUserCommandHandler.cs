using Dapper;
using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Domain;
using MediAR.Coreplatform.Infrastructure.Data;
using MediAR.Modules.Membership.Application.Configuration.Commands;
using MediAR.Modules.Membership.Application.Contracts;
using System;
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
    private readonly IInternalCommandScheduler _commandScheduler;

    public CreateUserCommandHandler(ISqlFacade sqlFacade, IExecutionContextAccessor executionContextAccessor, IInternalCommandScheduler commandScheduler)
    {
      _sqlFacade = sqlFacade;
      _executionContextAccessor = executionContextAccessor;
      _commandScheduler = commandScheduler;
    }

    public async Task<CreateUserCommandResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
      var passwordHash = PasswordManager.HashPassword(request.Password);

      var queryParams = new DynamicParameters();

      queryParams.Add("UserName", request.UserName);
      queryParams.Add("Email", request.Email);
      queryParams.Add("PasswordHash", passwordHash);
      queryParams.Add("FirstName", request.FirstName);
      queryParams.Add("LastName", request.LastName);
      queryParams.Add("TenantId", _executionContextAccessor.TenantId);
      queryParams.Add("InitialRole", "Student");
      queryParams.Add("UserId", dbType: DbType.Guid, direction: ParameterDirection.Output);

      try
      {
        await _sqlFacade.ExecuteAsync("[membership].[ins_User]", queryParams, commandType: CommandType.StoredProcedure);

        var userId = queryParams.Get<Guid>("UserId");
        await _commandScheduler.EnqueueAsync(new PublishUserWithRoleCreatedIntegrationEvent(userId, _executionContextAccessor.TenantId, request.UserName, request.Email, request.FirstName, request.LastName, "Student"));

        return new CreateUserCommandResult();
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
