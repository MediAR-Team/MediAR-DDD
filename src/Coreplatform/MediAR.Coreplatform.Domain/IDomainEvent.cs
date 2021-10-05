using System;

namespace MediAR.Coreplatform.Domain
{
  public interface IDomainEvent
  {
    Guid Id { get; }
    DateTime OccuredOn { get; }
  }
}
