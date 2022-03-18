using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.StudentSubmissions.Mapping
{
  public interface ISubmissionDataMapper
  {
    Task<object> MapDataAsync(string dataXml);
  }
}
