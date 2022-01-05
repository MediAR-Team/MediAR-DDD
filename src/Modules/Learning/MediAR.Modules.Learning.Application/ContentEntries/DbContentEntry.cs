using System;

namespace MediAR.Modules.Learning.Application.ContentEntries
{
  public class DbContentEntry
  {
    public int Id { get; set; }
    public Guid? TenantId { get; set; }
    public int? ModuleId { get; set; }
    public int? TypeId { get; set; }
    public string TypeName { get; set; }
    public string Configuration { get; set; }
    public string Data { get; set; }
  }
}
