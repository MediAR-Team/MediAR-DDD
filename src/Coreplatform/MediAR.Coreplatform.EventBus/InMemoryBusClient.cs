using MediAR.Coreplatform.Infrastructure.EventBus;
using System.Threading.Tasks;

namespace MediAR.Coreplatform.EventBus
{
  public class InMemoryBusClient : IEventBus
  {
    public void Dispose()
    {
    }

    public async Task Publish<T>(T @event) where T : IntegrationEvent
    {
      await InMemoryEventBus.Instance.Publish(@event);
    }

    public void Subscribe<T>(IIntegrationEventHandler<T> handler) where T : IntegrationEvent
    {
      InMemoryEventBus.Instance.Subscribe(handler);
    }
  }
}
