using MediAR.Modules.Learning.Application.Contracts;
using MediAR.Modules.Learning.Application.StudentSubmissions.GetSubmissionForEntry;
using System.Collections.Generic;

namespace MediAR.Modules.Learning.Application.StudentSubmissions.GetAllSubmissionsForEntry
{
  public class GetAllSubmissionsForEntryQuery : QueryBase<IEnumerable<StudentSubmissionDto>>
  {
    public int EntryId { get; }

    public GetAllSubmissionsForEntryQuery(int entryId)
    {
      EntryId = entryId;
    }
  }
}
