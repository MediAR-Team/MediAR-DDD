using MediAR.Modules.Membership.Application.Contracts;
using MediatR;

namespace MediAR.Modules.Membership.Application.Configuration.Queries
{
  public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult> { }
}
