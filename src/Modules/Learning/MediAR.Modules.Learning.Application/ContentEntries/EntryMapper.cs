using MediAR.Modules.Learning.Application.ContentEntries.EntryTypes.Lecture;
using MediAR.Modules.Learning.Application.ContentEntries.EntryTypes.SubmissionTask;
using System.IO;
using System.Xml.Serialization;

namespace MediAR.Modules.Learning.Application.ContentEntries
{
  public static class EntryMapper
  {
    public static (object data, object config) MapEntry(DbContentEntry raw)
    {
      XmlSerializer dataSerializer;
      XmlSerializer configSerializer;
      switch (raw.TypeName)
      {
        case "Lecture":
          dataSerializer = new XmlSerializer(typeof(LectureData));
          configSerializer = new XmlSerializer(typeof(LectureConfiguration));
          return (dataSerializer.Deserialize(new StringReader(raw.Data)), configSerializer.Deserialize(new StringReader(raw.Configuration)));
        case "SubmissionTask":
          dataSerializer = new XmlSerializer(typeof(SubmissionTaskData));
          configSerializer = new XmlSerializer(typeof(SubmissionTaskConfiguration));
          return (dataSerializer.Deserialize(new StringReader(raw.Data)), configSerializer.Deserialize(new StringReader(raw.Configuration)));
        default:
          return (null, null);
      }
    }
  }
}
