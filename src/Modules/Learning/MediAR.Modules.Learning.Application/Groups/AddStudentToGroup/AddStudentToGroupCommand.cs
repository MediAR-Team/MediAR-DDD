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

    public AddStudentToGroupCommand(int groupId, Guid studentId, Guid tenantId)
    {
      GroupId = groupId;
      StudentId = studentId;
      // TODO: investigate if we need tenant id or we can investigate it
      TenantId = tenantId;
    }

    public AddStudentToGroupCommand(int groupId, string studentUserName, Guid tenantId)
    {
      GroupId = groupId;
      StudentUserName = studentUserName;
      // TODO: investigate if we need tenant id or we can investigate it
      TenantId = tenantId;
    }

    public AddStudentToGroupCommand(int groupId, Guid studentId)
    {
      GroupId = groupId;
      StudentId = studentId;
    }

    public AddStudentToGroupCommand(int groupId, string studentUserName)
    {
      GroupId = groupId;
      StudentUserName = studentUserName;
    }

  }
}
