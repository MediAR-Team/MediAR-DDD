using System.Collections.Generic;

namespace MediAR.Modules.Learning.Application.ContentEntries.EntryTypeActions
{
  public class EntryTypeActionDto
  {
    public string Name { get; set; }

    public IList<ActionParamDto> Params { get; set; }
  }

  public class ActionParamDto
  {
    public string Name { get; set; }

    public string DataType { get; set; }
  }
}
