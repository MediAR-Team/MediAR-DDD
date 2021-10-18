using MediAR.Modules.TenantManagement.Application.Contracts;
using System;

namespace MediAR.Modules.TenantManagement.Application.Tenants.DeleteTenant
{
  public class DeleteTenantCommand : CommandBase
  {
    public Guid TenantId { get; }

    public DeleteTenantCommand(Guid tenantId)
    {
      TenantId = tenantId;
    }
  }
}
