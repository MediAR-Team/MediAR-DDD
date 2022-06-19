using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MediAR.Modules.Learning.Application.StudentSubmissions
{
  public interface IStudentSubmissionsRepository
  {
    Task SaveAsync<TData>(IStudentSubmission<TData> submission) where TData : class, IXmlSerializable;
    Task<DbStudentSubmission> GetForEntryAsync(int contentEntryId);
    Task<IEnumerable<DbStudentSubmission>> GetAllForEntryAsync(int contentEntryId);
  }
}
