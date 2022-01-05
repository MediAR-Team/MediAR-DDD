using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MediAR.Modules.Learning.Application.ContentEntries
{
  public interface IContentEntriesRepository
  {
    Task SaveEntryAsync<TData, TConfig>(IContentEntry<TData, TConfig> entry) where TData : IXmlSerializable where TConfig : IXmlSerializable;
    Task UpdateEntryAsync<TData, TConfig>(IContentEntry<TData, TConfig> entry) where TData : IXmlSerializable where TConfig : IXmlSerializable;
  }
}
