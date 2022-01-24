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
    private readonly ISqlFacade _sqlFacade;

    public UserAddedToRoleIntegrationEventHandler(ISqlFacade sqlFacade)
    {
      _sqlFacade = sqlFacade;
    }

    public async Task Handle(UserAddedToRoleIntegrationEvent notification, CancellationToken cancellationToken)
    {
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
          await _sqlFacade.ExecuteAsync("[learning].[ins_Student]", queryParams, commandType: CommandType.StoredProcedure);
          break;
        case "Instructor":
          await _sqlFacade.ExecuteAsync("[learning].[ins_Instructor]", queryParams, commandType: CommandType.StoredProcedure);
          break;
        default:
          break;
      }
    }
  }
}
