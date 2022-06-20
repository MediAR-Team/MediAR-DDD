using MediAR.Modules.Learning.Application.SubmittableEntries;
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MediAR.Modules.Learning.Application.ContentEntries.EntryTypes.SubmissionTask
{
  internal class SubmissionTaskContentEntry : IContentEntry<SubmissionTaskData, SubmissionTaskConfiguration>
  {
    public int? Id { get; }

    public Guid TenantId { get; }

    public int TypeId => 2;

    public int ModuleId { get; }

    public string Title { get; }

    public SubmissionTaskData Data { get; }

    public SubmissionTaskConfiguration Configuration { get; }

    public SubmissionTaskContentEntry(int? id, Guid tenantId, int moduleId, string title, SubmissionTaskData data, SubmissionTaskConfiguration configuration)
    {
      Id = id;
      TenantId = tenantId;
      ModuleId = moduleId;
      Title = title;
      Data = data;
      Configuration = configuration;
    }
  }

  public class SubmissionTaskConfiguration : IXmlSerializable, ISubmittableConfig
  {
    public int MaxMark { get; private set; }

    public DateTime DueTo { get; private set; }

    public SubmissionTaskConfiguration(int maxMark, DateTime dueTo)
    {
      MaxMark = maxMark;
      DueTo = dueTo;
    }

    public SubmissionTaskConfiguration(){}

    public XmlSchema GetSchema()
    {
      return null;
    }

    public void ReadXml(XmlReader reader)
    {
      reader.MoveToContent();
      MaxMark = int.Parse(reader.GetAttribute("maxmark"));
      DueTo = DateTime.Parse(reader.GetAttribute("dueto"));
    }

    public void WriteXml(XmlWriter writer)
    {
      writer.WriteAttributeString("maxmark", MaxMark.ToString());
      writer.WriteAttributeString("dueto", DueTo.ToString());
    }
  }

  public class SubmissionTaskData : IXmlSerializable
  {
    public string TextData { get; private set; }

    public SubmissionTaskData(string textData)
    {
      TextData = textData;
    }

    public SubmissionTaskData(){}

    public XmlSchema GetSchema()
    {
      return null;
    }

    public void ReadXml(XmlReader reader)
    {
      reader.MoveToContent();
      TextData = reader.GetAttribute("textdata") ?? string.Empty;
    }

    public void WriteXml(XmlWriter writer)
    {
      writer.WriteAttributeString("textdata", TextData);
    }
  }
}
