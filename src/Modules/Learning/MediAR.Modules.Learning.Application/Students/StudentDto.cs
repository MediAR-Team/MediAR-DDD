using System;

namespace MediAR.Modules.Learning.Application.Students
{
  public class StudentDto {
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }
}