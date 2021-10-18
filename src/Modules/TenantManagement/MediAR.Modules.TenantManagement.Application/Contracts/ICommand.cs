using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace MediAR.Modules.TenantManagement.Application.Contracts
{
  public interface ICommand<out TResult> : IRequest<TResult>
  {
    Guid Id { get; }
  }

  public interface ICommand : IRequest
  {
    Guid Id { get; }
  }
}
