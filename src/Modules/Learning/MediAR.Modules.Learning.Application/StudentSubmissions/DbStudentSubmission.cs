using System;

namespace MediAR.Modules.Learning.Application.StudentSubmissions
{
  public class DbStudentSubmission
  {
    public int Id { get; set; }
    public Guid TenantId { get; set; }
    public Guid StudentId { get; set; }
    public int EntryId { get; set; }
    public string Data { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ChangedAt { get; set; }
    public int TypeId { get; set; }
    public int SubmissionMarkMarkValue { get; set; }
    public string SubmissionMarkComment { get; set; }
    public DateTime? SubmissionMarkCreatedAt { get; set; }
    public DateTime? SubmissionMarkChangedAt { get; set; }
  }
}
