using MediAR.Modules.Learning.Application.Contracts;
using System.Collections.Generic;

namespace MediAR.Modules.Learning.Application.Modules.ReorderModules
{
  public class ReorderModulesCommand : CommandBase
  {
    public IEnumerable<OrderEntry> NewOrder { get; }

    public ReorderModulesCommand(IEnumerable<OrderEntry> newOrder)
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
