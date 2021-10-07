using System;

namespace MediAR.Coreplatform.Application.Tenants
{
  public class TenantConfiguration
  {
    public Guid MasterTenantId { get; set; }
    public Guid DefaultTenantId { get; set; }
  }
}
