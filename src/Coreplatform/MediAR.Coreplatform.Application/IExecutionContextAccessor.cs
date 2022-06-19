using System;
using System.Threading.Tasks;

namespace MediAR.Coreplatform.Application
{
  public interface IExecutionContextAccessor
  {
    public Guid UserId { get; }
    public Guid TenantId { get; }
    public bool IsInstructor { get; }
  }
}
