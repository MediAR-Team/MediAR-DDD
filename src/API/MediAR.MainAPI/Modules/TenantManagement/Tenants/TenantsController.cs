using MediAR.Modules.TenantManagement.Application.Contracts;
using MediAR.Modules.TenantManagement.Application.Tenants.CreateTenant;
using MediAR.Modules.TenantManagement.Application.Tenants.DeleteTenant;
using MediAR.Modules.TenantManagement.Application.Tenants.GetTenants;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MediAR.MainAPI.Modules.TenantManagement.Tenants
{
  [ApiController]
  [Route("api/tenantmanagement/[controller]")]
  public class TenantsController : ControllerBase
  {
    private readonly ITenantManagementModule _tenantManagementModule;

    public TenantsController(ITenantManagementModule tenantManagementModule)
    {
      _tenantManagementModule = tenantManagementModule;
    }

    [HttpGet]
    public async Task<IActionResult> GetTenants([FromQuery] GetTenantsRequest request)
    {
      var result = await _tenantManagementModule.ExecuteQueryAsync(new GetTenantsQuery(request.Page, request.PageSize));

      return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTenant(CreateTenantRequest request)
    {
      var result = await _tenantManagementModule.ExecuteCommandAsync(new CreateTenantCommand(request.Name, request.ConnectionString));

      return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTenant(Guid id)
    {
      await _tenantManagementModule.ExecuteCommandAsync(new DeleteTenantCommand(id));

      return Ok();
    }
  }
}
