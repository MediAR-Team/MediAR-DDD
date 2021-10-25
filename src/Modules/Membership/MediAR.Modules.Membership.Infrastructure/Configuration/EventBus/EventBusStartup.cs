using Autofac;
using MediAR.Coreplatform.Infrastructure.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Infrastructure.Configuration.EventBus
{
  public static class EventBusStartup
  {
    public static void Initialize()
    {
      SubscribeToIntegrationEvents();
    }

    private static void SubscribeToIntegrationEvents()
    {
      using var scope = MembershipCompositionRoot.BeginLifetimeScope();
      var eventBus = scope.Resolve<IEventBus>();
    }

    private static void SubscribeToIntegrationEvent<T>(IEventBus eventBus) where T : IntegrationEvent
    {
      eventBus.Subscribe(new IntegrationEventGenericHandler<T>());
    }
  }
}
