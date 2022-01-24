using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Membership.Application.Configuration.Commands;
using MediAR.Modules.Membership.Application.Contracts;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Application.Users.AssignRoleToUser
{
  class AssignRoleToUserCommandHandler : ICommandHandler<AssignRoleToUserCommand, AssignRoleToUserCommandResult>
  {
    private readonly ISqlFacade _sqlFacade;
    private readonly IInternalCommandScheduler _commandScheduler;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public AssignRoleToUserCommandHandler(ISqlFacade sqlFacade, IInternalCommandScheduler commandScheduler, IExecutionContextAccessor executionContextAccessor)
    {
      _sqlFacade = sqlFacade;
      _commandScheduler = commandScheduler;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<AssignRoleToUserCommandResult> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
      AssignRoleToUserCommandResult result = new();

      switch (request.IdentifierOption)
      {
        case UserIdentifierOption.UserName:
          result = await AssignRoleToUserByUserName(request);
          break;
        case UserIdentifierOption.Email:
          result = await AssignRoleToUserByEmail(request);
          break;
      }

      await _commandScheduler.EnqueueAsync(new PublishUserAddedToRoleIntegrationEventCommand(request.UserIdentifier, request.RoleName));

      return result;
    }

    private async Task<AssignRoleToUserCommandResult> AssignRoleToUserByUserName(AssignRoleToUserCommand request)
    {
      try
      {
        await _sqlFacade.ExecuteAsync("[membership].[assign_Role_to_User_by_UserName]", new { UserName = request.UserIdentifier, request.RoleName, _executionContextAccessor.TenantId }, commandType: CommandType.StoredProcedure);
        return new AssignRoleToUserCommandResult();
      }
      catch (Exception ex)
      {
        return new AssignRoleToUserCommandResult(ex.Message);
      }
    }

    private async Task<AssignRoleToUserCommandResult> AssignRoleToUserByEmail(AssignRoleToUserCommand request)
    {
      try
      {
        await _sqlFacade.ExecuteAsync("[membership].[assign_Role_to_User_by_Email]", new { Email = request.UserIdentifier, request.RoleName, _executionContextAccessor.TenantId }, commandType: CommandType.StoredProcedure);
        return new AssignRoleToUserCommandResult();
      }
      catch (SqlException ex)
      {
        return new AssignRoleToUserCommandResult(ex.Message);
      }
    }
  }
}
