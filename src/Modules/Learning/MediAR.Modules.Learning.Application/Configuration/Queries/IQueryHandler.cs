using MediAR.Modules.Learning.Application.Contracts;
using MediatR;

namespace MediAR.Modules.Learning.Application.Configuration.Queries
{
  public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult> { }
}
