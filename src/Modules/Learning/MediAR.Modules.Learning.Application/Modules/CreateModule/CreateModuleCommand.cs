using MediAR.Modules.Learning.Application.Contracts;
using System;

namespace MediAR.Modules.Learning.Application.Modules.CreateModule
{
  public class CreateModuleCommand : CommandBase<ModuleDto>
  {
    public string Name { get; }
    public int CourseId { get; }
    public Guid? TenantId { get; }

    public CreateModuleCommand(string name, int moduleId, Guid? tenantId)
    {
      Name = name;
      CourseId = moduleId;
      TenantId = tenantId;
    }

    public CreateModuleCommand(string name, int moduleId) : this(name, moduleId, null) { }
  }
}
