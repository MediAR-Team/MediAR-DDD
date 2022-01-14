using MediAR.Modules.Learning.Application.Contracts;
using System;

namespace MediAR.Modules.Learning.Application.Modules.CreateModule
{
  public class CreateModuleCommand : CommandBase<ModuleDto>
  {
    public string Name { get; }
    public int CourseId { get; }
    public Guid? TenantId { get; }

    public CreateModuleCommand(string name, int courseId, Guid? tenantId = null)
    {
      Name = name;
      CourseId = courseId;
      TenantId = tenantId;
    }
  }
}
