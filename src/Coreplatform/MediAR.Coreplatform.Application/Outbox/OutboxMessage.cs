using System;

namespace MediAR.Coreplatform.Application.Outbox
{
  public class OutboxMessage
  {
    public Guid Id { get; }
    public DateTime OccuredOn { get; }
    public string Type { get; }
    public string Data { get; }
    public DateTime? ProcessedDate { get; }

    public OutboxMessage(Guid id, DateTime occuredOn, string type, string data)
    {
      Id = id;
      OccuredOn = occuredOn;
      Type = type;
      Data = data;
    }

    private OutboxMessage() { }
  }
}
