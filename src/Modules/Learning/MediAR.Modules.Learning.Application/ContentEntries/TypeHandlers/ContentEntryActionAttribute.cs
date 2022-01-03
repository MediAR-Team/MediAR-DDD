using System;

namespace MediAR.Modules.Learning.Application.ContentEntries.TypeHandlers
{
  [AttributeUsage(AttributeTargets.Method)]
  class ContentEntryActionAttribute : Attribute
  {
    public string ActionName { get; }

    public ContentEntryActionAttribute(string actionName)
    {
      ActionName = actionName;
    }
  }
}
