using Autofac;
using MediAR.Coreplatform.EventBus;
using MediAR.Coreplatform.Infrastructure.EventBus;

namespace MediAR.Modules.Membership.Infrastructure.Configuration.EventBus
{
  class EventBusModule : Module
  {
    private readonly IEventBus _eventBus;

    public EventBusModule(IEventBus eventBus)
    {
      _eventBus = eventBus;
    }

    protected override void Load(ContainerBuilder builder)
    {
      if (_eventBus != null)
      {
        builder.RegisterInstance(_eventBus);
      }
      else
      {
        builder.RegisterType<InMemoryBusClient>()
          .As<IEventBus>()
          .SingleInstance();
      }
    }
  }
}
