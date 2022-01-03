using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MediAR.Modules.Learning.Application.ContentEntries.EntryTypes.Lecture
{
  class LectureContentEntry : IContentEntry<LectureData, LectureConfiguration>
  {
    public int? Id { get; set; }

    public Guid TenantId { get; set; }

    public int TypeId => 1;

    public int ModuleId { get; set; }

    public LectureData Data { get; set; }

    public LectureConfiguration Configuration { get; set; }

    public LectureContentEntry(int? id, Guid tenantId, int moduleId, LectureData data, LectureConfiguration configuration)
    {
      Id = id;
      TenantId = tenantId;
      ModuleId = moduleId;
      Data = data;
      Configuration = configuration;
    }

    public LectureContentEntry(){}
  }

  public class LectureConfiguration : IXmlSerializable
  {
    public XmlSchema GetSchema()
    {
      return null;
    }

    public void ReadXml(XmlReader reader) {}

    public void WriteXml(XmlWriter writer) {}
  }

  public class LectureData : IXmlSerializable
  {
    public string Text { get; set; }

    public LectureData(){}

    public LectureData(string text)
    {
      Text = text;
    }

    public XmlSchema GetSchema()
    {
      return null;
    }

    public void ReadXml(XmlReader reader)
    {
      reader.MoveToContent();
      Text = reader.GetAttribute("text") ?? string.Empty;
    }

    public void WriteXml(XmlWriter writer)
    {
      writer.WriteAttributeString("text", Text);
    }
  }
}
