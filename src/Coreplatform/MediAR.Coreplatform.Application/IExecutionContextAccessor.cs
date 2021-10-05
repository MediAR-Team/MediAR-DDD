using System;

namespace MediAR.Coreplatform.Application
{
  public interface IExecutionContextAccessor
  {
    public Guid UserId { get; }
    public Guid TenantId { get; }
  }
}
