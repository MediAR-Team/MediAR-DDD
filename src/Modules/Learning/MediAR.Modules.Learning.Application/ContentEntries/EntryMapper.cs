using MediAR.Modules.Learning.Application.ContentEntries.EntryTypes.Lecture;
using System.IO;
using System.Xml.Serialization;

namespace MediAR.Modules.Learning.Application.ContentEntries
{
  public static class EntryMapper
  {
    public static (object data, object config) MapEntry(DbContentEntry raw)
    {
      switch (raw.TypeName)
      {
        case "Lecture":
          var dataSerializer = new XmlSerializer(typeof(LectureData));
          var configSerializer = new XmlSerializer(typeof(LectureConfiguration));
          return (dataSerializer.Deserialize(new StringReader(raw.Data)), configSerializer.Deserialize(new StringReader(raw.Configuration)));
        default:
          return (null, null);
      }
    }
  }
}
