using MediAR.Coreplatform.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediAR.Coreplatform.Infrastructure.DomainEvents
{
  public interface IDomainEventsAccessor
  {
    Task<IReadOnlyCollection<IDomainEvent>> GetAllDomainEvents();
    Task ClearAllDomainEvents();
  }
}
