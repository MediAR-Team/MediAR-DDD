using MediAR.Modules.Learning.Application.Contracts;

namespace MediAR.Modules.Learning.Application.SubmissionMarks.CreateSubmissionMark
{
  public class CreateSubmissionMarkCommand : CommandBase
  {
    public int Submissionid { get; }
    public int Mark { get; }
    public string Comment { get; }

    public CreateSubmissionMarkCommand(int submissionId, int mark, string comment)
    {
      Submissionid = submissionId;
      Mark = mark;
      Comment = comment;
    }
  }
}
