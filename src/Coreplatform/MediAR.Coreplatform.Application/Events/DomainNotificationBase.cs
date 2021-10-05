using MediAR.Coreplatform.Domain;
using System;

namespace MediAR.Coreplatform.Application.Events
{
  class DomainNotificationBase<T> : IDomainEventNotification<T> where T : IDomainEvent
  {
    public T DomainEvent { get; }

    public Guid Id { get; }

    public DomainNotificationBase(T @event, Guid id)
    {
      DomainEvent = @event;
      Id = id;
    }
  }
}
