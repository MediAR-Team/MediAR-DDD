using System;

namespace MediAR.Modules.Learning.Application.StudentSubmissions.GetSubmissionForEntry
{
  public class StudentSubmissionDto
  {
    public int Id { get; set; }
    public int EntryId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ChangedAt { get; set; }

    public object Data { get; set; }
  }
}
