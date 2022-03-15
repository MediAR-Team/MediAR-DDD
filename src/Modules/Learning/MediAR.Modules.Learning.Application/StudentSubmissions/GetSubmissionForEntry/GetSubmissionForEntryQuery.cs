using MediAR.Modules.Learning.Application.Contracts;

namespace MediAR.Modules.Learning.Application.StudentSubmissions.GetSubmissionForEntry
{
  public class GetSubmissionForEntryQuery : QueryBase<StudentSubmissionDto>
  {
    public int EntryId { get; }

    public GetSubmissionForEntryQuery(int entryId)
    {
      EntryId = entryId;
    }
  }
}
