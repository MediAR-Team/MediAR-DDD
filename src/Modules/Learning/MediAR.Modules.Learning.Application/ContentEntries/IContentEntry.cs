using System;
using System.Xml.Serialization;

namespace MediAR.Modules.Learning.Application.ContentEntries
{
  public interface IContentEntry<TData, TConfig> where TData : IXmlSerializable where TConfig : IXmlSerializable
  {
    // TODO use different models for create and fetch maybe somehow
    public int? Id { get; }
    public Guid TenantId { get; }
    public int TypeId { get; }
    public int ModuleId { get; }
    public TData Data { get; }
    public TConfig Configuration { get; }
  }
}
