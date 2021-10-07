using System.Threading.Tasks;

namespace MediAR.Coreplatform.Infrastructure.DomainEvents
{
  public interface IDomainEventsDispatcher
  {
    Task DispatchEventsAsync();
  }
}
