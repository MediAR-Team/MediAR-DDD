using System.Threading.Tasks;

namespace MediAR.Coreplatform.Infrastructure.EventBus
{
  public interface IIntegrationEventHandler<in T> : IIntegrationEventHandler where T : IntegrationEvent
  {
    Task Handle(T @event);
  }

  public interface IIntegrationEventHandler { }
}