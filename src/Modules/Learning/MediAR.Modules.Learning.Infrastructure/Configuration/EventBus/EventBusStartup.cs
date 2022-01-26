using Autofac;
using MediAR.Coreplatform.Infrastructure.EventBus;
using MediAR.Modules.Membership.IntegrationEvents;

namespace MediAR.Modules.Learning.Infrastructure.Configuration.EventBus
{
  public static class EventBusStartup
  {
    public static void Initialize()
    {
      SubscribeToIntegrationEvents();
    }

    private static void SubscribeToIntegrationEvents()
    {
      using var scope = LearningCompositionRoot.BeginLifetimeScope();
      var eventBus = scope.Resolve<IEventBus>();

      SubscribeToIntegrationEvent<UserAddedToRoleIntegrationEvent>(eventBus);
      SubscribeToIntegrationEvent<UserCreatedWithRoleIntegrationEvent>(eventBus);
    }

    private static void SubscribeToIntegrationEvent<T>(IEventBus eventBus) where T : IntegrationEvent
    {
      eventBus.Subscribe(new IntegrationEventGenericHandler<T>());
    }
  }
}
