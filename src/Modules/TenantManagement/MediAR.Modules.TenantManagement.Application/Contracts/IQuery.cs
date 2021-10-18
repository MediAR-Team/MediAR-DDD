using MediatR;
using System;

namespace MediAR.Modules.TenantManagement.Application.Contracts
{
  public interface IQuery<out TResult> : IRequest<TResult>
  {
    Guid Id { get; }
  }
}
