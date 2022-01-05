using MediAR.Modules.Learning.Application.Contracts;
using System.Collections.Generic;

namespace MediAR.Modules.Learning.Application.ContentEntries.EntryTypeActions
{
  public class EntryTypeActionsQuery : QueryBase<List<EntryTypeActionDto>>
  {
    public string EntryTypeName { get; }

    public EntryTypeActionsQuery(string entryTypeName)
    {
      EntryTypeName = entryTypeName;
    }
  }
}
