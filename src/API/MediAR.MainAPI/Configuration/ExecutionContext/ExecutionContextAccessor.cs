using MediAR.Coreplatform.Application;
using Microsoft.AspNetCore.Http;
using System;

namespace MediAR.MainAPI.Configuration.ExecutionContext
{
  public class ExecutionContextAccessor : IExecutionContextAccessor
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
      get
      {
        if (_httpContextAccessor.HttpContext?.User?.FindFirst("sub") is not null)
        {
          return Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst("sub").Value);
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

        throw new ApplicationException("Tenant context is not available");
      }
    }
  }
}
