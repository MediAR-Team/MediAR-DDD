using System;
using System.Xml.Serialization;

namespace MediAR.Modules.Learning.Application.StudentSubmissions
{
  public interface IStudentSubmission<TData> where TData : class, IXmlSerializable
  {
    public int? Id { get; }
    public Guid TenantId { get; }
    public Guid StudentId { get; }
    public int EntryId { get; }
    public TData Data { get; }
    public DateTime CreatedAt { get; }
    public DateTime? ChangedAt { get; }
    public int ContentEntryType { get; }
  }
}
