using MediAR.Modules.Learning.Application.Contracts;
using System;

namespace MediAR.Modules.Learning.Application.Groups.AddStudentToGroup
{
  public class AddStudentToGroupCommand : CommandBase
  {
    public int GroupId { get; }
    public Guid StudentId { get; }
    public string StudentUserName { get; }
    public Guid? TenantId { get; }

    public AddStudentToGroupCommand(int groupId, Guid studentId)
    {
      GroupId = groupId;
      StudentId = studentId;
    }
  }
}
