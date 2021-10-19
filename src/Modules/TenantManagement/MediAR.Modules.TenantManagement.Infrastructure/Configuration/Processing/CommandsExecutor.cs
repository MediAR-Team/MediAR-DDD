using Autofac;
using MediAR.Modules.TenantManagement.Application.Contracts;
using MediAR.Modules.TenantManagement.Infrastructure.Configuration;
using MediatR;
using System.Threading.Tasks;

namespace MediAR.Modules.TenantManagement.Infrastructure.Configuration.Processing
{
  static class CommandsExecutor
  {
    internal static async Task Execute(ICommand command)
    {
      using var scope = TenantManagementCompositionRoot.BeginLifetimeScope();
      var mediator = scope.Resolve<IMediator>();
      await mediator.Send(command);
    }

    internal static async Task<TResult> Execute<TResult>(ICommand<TResult> command)
    {
      using var scope = TenantManagementCompositionRoot.BeginLifetimeScope();
      var mediator = scope.Resolve<IMediator>();
      return await mediator.Send(command);
    }
  }
}
