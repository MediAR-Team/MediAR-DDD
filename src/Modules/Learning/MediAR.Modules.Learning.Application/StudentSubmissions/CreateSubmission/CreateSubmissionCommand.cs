using MediAR.Modules.Learning.Application.Contracts;

namespace MediAR.Modules.Learning.Application.StudentSubmissions.CreateSubmission
{
  public class CreateSubmissionCommand : CommandBase<object>
  {
    public int EntryId { get; set; }

    public dynamic Payload { get; set; }

    public CreateSubmissionCommand(int entryId, dynamic payload)
    {
      EntryId = entryId;
      Payload = payload;
    }
  }
}
