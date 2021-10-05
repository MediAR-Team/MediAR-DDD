using System;

namespace MediAR.Coreplatform.Domain
{
  public class DomainEventBase : IDomainEvent
  {
    public Guid Id { get; }

    public DateTime OccuredOn { get; }

    public DomainEventBase()
    {
      Id = Guid.NewGuid();
      OccuredOn = DateTime.Now;
    }
  }
}
