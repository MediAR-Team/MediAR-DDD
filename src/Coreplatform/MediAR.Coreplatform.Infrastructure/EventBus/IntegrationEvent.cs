using MediatR;
using System;

namespace MediAR.Coreplatform.Infrastructure.EventBus
{
  public abstract class IntegrationEvent : INotification
  {
    public Guid Id { get; }
    public DateTime OccuredOn { get; }

    public IntegrationEvent(Guid id, DateTime occuredOn)
    {
      Id = id;
      OccuredOn = occuredOn;
    }
  }
}