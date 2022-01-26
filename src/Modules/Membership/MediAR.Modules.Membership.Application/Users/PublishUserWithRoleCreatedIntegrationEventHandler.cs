using MediAR.Coreplatform.Infrastructure.EventBus;
using MediAR.Modules.Membership.Application.Configuration.Commands;
using MediAR.Modules.Membership.IntegrationEvents;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Application.Users
{
  internal class PublishUserWithRoleCreatedIntegrationEventHandler : ICommandHandler<PublishUserWithRoleCreatedIntegrationEvent>
  {
    private readonly IEventBus _eventBus;

    public PublishUserWithRoleCreatedIntegrationEventHandler(IEventBus eventBus)
    {
      _eventBus = eventBus;
    }

    public async Task<Unit> Handle(PublishUserWithRoleCreatedIntegrationEvent request, CancellationToken cancellationToken)
    {
      await _eventBus.Publish(new UserCreatedWithRoleIntegrationEvent(Guid.NewGuid(), DateTime.UtcNow, request.UserId, request.Email, request.UserName, request.FirstName, request.LastName, request.RoleName, request.TenantId));

      return Unit.Value;
    }
  }
}
