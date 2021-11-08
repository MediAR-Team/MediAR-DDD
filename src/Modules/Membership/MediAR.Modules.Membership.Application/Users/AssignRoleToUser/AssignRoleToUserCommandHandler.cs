using Dapper;
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
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IInternalCommandScheduler _commandScheduler;

    public AssignRoleToUserCommandHandler(ISqlConnectionFactory connectionFactory, IInternalCommandScheduler commandScheduler)
    {
      _connectionFactory = connectionFactory;
      _commandScheduler = commandScheduler;
    }

    public async Task<AssignRoleToUserCommandResult> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();

      AssignRoleToUserCommandResult result = new();

      switch (request.IdentifierOption)
      {
        case UserIdentifierOption.UserName:
          result = await AssignRoleToUserByUserName(request, connection);
          break;
        case UserIdentifierOption.Email:
          result = await AssignRoleToUserByEmail(request, connection);
          break;
      }

      await _commandScheduler.EnqueueAsync(new PublishUserAddedToRoleIntegrationEventCommand(request.UserIdentifier, request.RoleName));

      return result;
    }

    private async Task<AssignRoleToUserCommandResult> AssignRoleToUserByUserName(AssignRoleToUserCommand request, IDbConnection connection)
    {
      try
      {
        await connection.QueryAsync("[membership].[assign_Role_to_User_by_UserName]", new { UserName = request.UserIdentifier, request.RoleName }, commandType: CommandType.StoredProcedure);
        return new AssignRoleToUserCommandResult();
      }
      catch (Exception ex)
      {
        return new AssignRoleToUserCommandResult(ex.Message);
      }
    }

    private async Task<AssignRoleToUserCommandResult> AssignRoleToUserByEmail(AssignRoleToUserCommand request, IDbConnection connection)
    {
      try
      {
        await connection.QueryAsync("[membership].[assign_Role_to_User_by_Email]", new { Email = request.UserIdentifier, request.RoleName }, commandType: CommandType.StoredProcedure);
        return new AssignRoleToUserCommandResult();
      }
      catch (SqlException ex)
      {
        return new AssignRoleToUserCommandResult(ex.Message);
      }
    }
  }
}
