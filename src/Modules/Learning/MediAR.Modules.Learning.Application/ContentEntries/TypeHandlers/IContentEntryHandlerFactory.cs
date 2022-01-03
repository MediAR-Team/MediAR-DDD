using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.ContentEntries.TypeHandlers
{
  public interface IContentEntryHandlerFactory
  {
    Task<IContentEntryHandler> GetHandlerAsync(int typeId);
    Task<IContentEntryHandler> GetHandlerAsync(string typeName);
  }
}
