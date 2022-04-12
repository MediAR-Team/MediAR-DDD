namespace MediAR.MainAPI.Modules.Learning.SubmissionMarks
{
  public class CreateSubmissionMarkRequest
  {
    public int SubmissionId { get; set; }
    public int Mark { get; set; }
    public string Comment { get; set; }
  }
}
