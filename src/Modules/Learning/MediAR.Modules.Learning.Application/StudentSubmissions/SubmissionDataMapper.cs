using MediAR.Modules.Learning.Application.StudentSubmissions.SubmissionTypes;
using System.IO;
using System.Xml.Serialization;

namespace MediAR.Modules.Learning.Application.StudentSubmissions
{
  internal static class SubmissionDataMapper
  {
    public static object MapData(int typeId, string dataXml)
    {
      switch (typeId)
      {
        case 2:
          var dataSerializer = new XmlSerializer(typeof(SubmissionTaskSubmissionData));
          return dataSerializer.Deserialize(new StringReader(dataXml));
        default:
          return null;
      }
    }
  }
}
