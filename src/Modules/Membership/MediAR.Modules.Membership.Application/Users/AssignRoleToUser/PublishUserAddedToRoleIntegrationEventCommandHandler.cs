using Dapper;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Infrastructure.EventBus;
using MediAR.Modules.Membership.Application.Configuration.Commands;
using MediAR.Modules.Membership.IntegrationEvents;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Application.Users.AssignRoleToUser
{
  class PublishUserAddedToRoleIntegrationEventCommandHandler : ICommandHandler<PublishUserAddedToRoleIntegrationEventCommand>
  {
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IEventBus _eventBus;

    public PublishUserAddedToRoleIntegrationEventCommandHandler(ISqlConnectionFactory connectionFactory, IEventBus eventBus)
    {
      _connectionFactory = connectionFactory;
      _eventBus = eventBus;
    }

    public async Task<Unit> Handle(PublishUserAddedToRoleIntegrationEventCommand request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();

      const string sql = @"SELECT
                          [User].[Id],
                          [User].[UserName],
                          [User].[Email],
                          [User].[FirstName],
                          [User].[LastName],
                          [User].[TenantId]
                          FROM [membership].[v_Users] [User]
                          WHERE [UserName] = @Identifier OR [Email] = @Identifier";

      var user = await connection.QuerySingleOrDefaultAsync<UserDto>(sql, new { Identifier = request.UserNameOrEmail });

      await _eventBus.Publish(new UserAddedToRoleIntegrationEvent(Guid.NewGuid(), DateTime.Now, user.Id, user.Email, user.UserName, user.FirstName, user.LastName, request.RoleName, user.TenantId));

      return Unit.Value;
    }
  }
}
