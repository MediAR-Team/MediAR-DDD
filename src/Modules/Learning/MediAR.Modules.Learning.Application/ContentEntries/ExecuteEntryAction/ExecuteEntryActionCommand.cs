using MediAR.Modules.Learning.Application.Contracts;

namespace MediAR.Modules.Learning.Application.ContentEntries.ExecuteEntryAction
{
  public class ExecuteEntryActionCommand : CommandBase<object>
  {
    public string EntryType { get; }

    public string ActionName { get; }

    public dynamic Payload { get; }

    public ExecuteEntryActionCommand(string entryType, string actionName, dynamic payload)
    {
      EntryType = entryType;
      ActionName = actionName;
      Payload = payload;
    }
  }
}
