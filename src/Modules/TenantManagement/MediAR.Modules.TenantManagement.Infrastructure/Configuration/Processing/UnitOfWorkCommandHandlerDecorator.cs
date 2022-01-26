using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.TenantManagement.Application.Configuration.Commands;
using MediAR.Modules.TenantManagement.Application.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.TenantManagement.Infrastructure.Configuration.Processing
{
  internal class UnitOfWorkCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
  {
    private readonly ICommandHandler<T> _decorated;
    private readonly ISqlFacade _sqlFacade;

    public UnitOfWorkCommandHandlerDecorator(ICommandHandler<T> decorated, ISqlFacade sqlFacade)
    {
      _decorated = decorated;
      _sqlFacade = sqlFacade;
    }

    public async Task<Unit> Handle(T request, CancellationToken cancellationToken)
    {
      var result = await _decorated.Handle(request, cancellationToken);
      await _sqlFacade.CommitAsync();
      return result;
    }
  }

  internal class UnitOfWorkCommandHandlerDecorator<T, TReturn> : ICommandHandler<T, TReturn> where T : ICommand<TReturn>
  {
    private readonly ICommandHandler<T, TReturn> _decorated;
    private readonly ISqlFacade _sqlFacade;

    public UnitOfWorkCommandHandlerDecorator(ICommandHandler<T, TReturn> decorated, ISqlFacade sqlFacade)
    {
      _decorated = decorated;
      _sqlFacade = sqlFacade;
    }

    public async Task<TReturn> Handle(T request, CancellationToken cancellationToken)
    {
      var result = await _decorated.Handle(request, cancellationToken);
      await _sqlFacade.CommitAsync();
      return result;
    }
  }
}
