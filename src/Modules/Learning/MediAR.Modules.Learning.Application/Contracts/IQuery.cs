using MediatR;
using System;

namespace MediAR.Modules.Learning.Application.Contracts
{
  public interface IQuery<out TResult> : IRequest<TResult>
  {
    Guid Id { get; }
  }
}
