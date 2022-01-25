using MediAR.Coreplatform.Application;
using MediAR.Modules.TenantManagement.Application.Contracts;
using MediAR.Modules.TenantManagement.Application.Tenants.GetTenantByRefer;
using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MediAR.MainAPI.Configuration.ExecutionContext
{
  public class ExecutionContextAccessor : IExecutionContextAccessor
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITenantManagementModule _tenantManagementModule;

    public ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor, ITenantManagementModule tenantManagementModule)
    {
      _httpContextAccessor = httpContextAccessor;
      _tenantManagementModule = tenantManagementModule;
    }

    public Guid UserId
    {
      get
      {
        if (_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier) is not null)
        {
          return Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        throw new ApplicationException("User context is not available");
      }
    }

    public Guid TenantId
    {
      get
      {
        if (_httpContextAccessor.HttpContext.User.FindFirst("tenantId") is not null)
        {
          return Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst("tenantId").Value);
        }
        // TODO: maybe wrap it in a chain of responsibility
        else if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Origin", out var referUrl))
        {
          var tenant = _tenantManagementModule.ExecuteQueryAsync(new GetTenantByReferQuery(referUrl)).GetAwaiter().GetResult();

          return tenant.Id;
        }

        throw new ApplicationException("Tenant context is not available");
      }
    }
  }
}
