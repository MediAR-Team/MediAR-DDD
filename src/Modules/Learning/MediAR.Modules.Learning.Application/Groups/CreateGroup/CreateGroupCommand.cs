using MediAR.Modules.Learning.Application.Contracts;
using System;

namespace MediAR.Modules.Learning.Application.Groups.CreateGroup
{
  public class CreateGroupCommand : CommandBase<GroupDto>
  {
    public string Name { get; }
    public Guid? TenantId { get; }

    public CreateGroupCommand(string name)
    {
      Name = name;
    }

    public CreateGroupCommand(string name, Guid tenantId)
    {
      Name = name;
      TenantId = tenantId;
    }
  }
}
