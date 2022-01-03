using MediAR.Modules.Learning.Application.Contracts;
using System;

namespace MediAR.Modules.Learning.Application.Modules.DeleteModule
{
  public class DeleteModuleCommand : CommandBase
  {
    public int ModuleId { get; }
    public Guid? TenantId { get; }

    public DeleteModuleCommand(int moduleId, Guid? tenantId)
    {
      ModuleId = moduleId;
      TenantId = tenantId;
    }

    public DeleteModuleCommand(int moduleId) : this(moduleId, null) { }
  }
}
