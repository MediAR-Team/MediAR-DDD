using MediAR.Modules.Learning.Application.Contracts;
using System.Collections.Generic;

namespace MediAR.Modules.Learning.Application.ContentEntries.ReorderContentEntries
{
  public class ReorderContentEntriesCommand : CommandBase
  {
    public IEnumerable<OrderEntry> NewOrder { get; }

    public ReorderContentEntriesCommand(IEnumerable<OrderEntry> newOrder)
    {
      NewOrder = newOrder;
    }
  }

  public class OrderEntry
  {
    public int Id { get; }
    public int Ordinal { get; }

    public OrderEntry(int id, int ordinal)
    {
      Id = id;
      Ordinal = ordinal;
    }
  }
}
