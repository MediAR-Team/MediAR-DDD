using MediAR.Coreplatform.Infrastructure.EventBus;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediAR.Coreplatform.EventBus
{
  public class InMemoryEventBus
  {
    private InMemoryEventBus()
    {
      _handlerMapping = new Dictionary<string, List<IIntegrationEventHandler>>();
    }

    public static InMemoryEventBus Instance { get; } = new InMemoryEventBus();

    private readonly IDictionary<string, List<IIntegrationEventHandler>> _handlerMapping;

    public void Subscribe<T>(IIntegrationEventHandler<T> handler) where T : IntegrationEvent
    {
      var type = typeof(T).FullName;
      if (type != null)
      {
        if (_handlerMapping.ContainsKey(type))
        {
          _handlerMapping[type].Add(handler);
        }
        else
        {
          _handlerMapping.Add(type, new List<IIntegrationEventHandler> { handler });
        }
      }
    }

    public async Task Publish<T>(T @event) where T : IntegrationEvent
    {
      var type = @event.GetType().FullName;

      if (type != null)
      {
        var handlers = _handlerMapping[type];

        foreach (var handler in handlers)
        {
          if (handler is IIntegrationEventHandler<T> typedHandler)
          {
            await typedHandler.Handle(@event);
          }
        }
      }
    }
  }
}
