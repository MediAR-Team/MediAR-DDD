using MediAR.Modules.Learning.Application.Contracts;
using System;

namespace MediAR.Modules.Learning.Application.Groups.DeleteGroup
{
  public class DeleteGroupCommand : CommandBase
  {
    public int GroupId { get; }
    public Guid? TenantId { get; }

    public DeleteGroupCommand(int groupId)
    {
      GroupId = groupId;
    }

    public DeleteGroupCommand(int groupId, Guid tenantId)
    {
      GroupId = groupId;
      TenantId = tenantId;
    }
  }
}
