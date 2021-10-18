using System;

namespace MediAR.Modules.TenantManagement.Application.Tenants
{
  public class TenantDto
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ConnectionString { get; set; }
  }
}
