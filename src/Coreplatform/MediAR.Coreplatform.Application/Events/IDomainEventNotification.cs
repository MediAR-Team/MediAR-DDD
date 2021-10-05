using MediAR.Coreplatform.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAR.Coreplatform.Application.Events
{
  public interface IDomainEventNotification<T> where T : IDomainEvent
  {
    T DomainEvent { get; }
    public Guid Id { get; }
  }
}
