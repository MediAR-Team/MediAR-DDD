using MediAR.Modules.TenantManagement.Application.Contracts;
using MediatR;

namespace MediAR.Modules.TenantManagement.Application.Configuration.Queries
{
  public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult> { }
}
