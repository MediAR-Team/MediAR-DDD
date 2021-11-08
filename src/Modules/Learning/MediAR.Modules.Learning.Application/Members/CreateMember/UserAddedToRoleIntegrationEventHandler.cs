using Dapper;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Membership.IntegrationEvents;
using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Members.CreateMember
{
  class UserAddedToRoleIntegrationEventHandler : INotificationHandler<UserAddedToRoleIntegrationEvent>
  {
    private readonly ISqlConnectionFactory _connectionFactory;

    public UserAddedToRoleIntegrationEventHandler(ISqlConnectionFactory connectionFactory)
    {
      _connectionFactory = connectionFactory;
    }

    public async Task Handle(UserAddedToRoleIntegrationEvent notification, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();

      var queryParams = new
      {
        notification.UserId,
        notification.UserName,
        notification.Email,
        notification.FirstName,
        notification.LastName,
        notification.TenantId
      };

      switch(notification.RoleName)
      {
        case "Student":
          await connection.ExecuteScalarAsync("[learning].[ins_Student]", queryParams, commandType: CommandType.StoredProcedure);
          break;
        default:
          break;
      }
    }
  }
}
