using System;

namespace MediAR.Modules.Learning.Application.Modules.CreateModule
{
  public class ModuleDto
  {
    public int Id { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; }
  }
}
