using Autofac;
using MediAR.Modules.TenantManagement.Application.Contracts;
using MediAR.Modules.TenantManagement.Infrastructure.Configuration;
using MediAR.Modules.TenantManagement.Infrastructure.Configuration.Processing;
using MediatR;
using System.Threading.Tasks;

namespace MediAR.Modules.TenantManagement.Infrastructure
{
  public class TenantManagementModule : ITenantManagementModule
  {
    public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
    {
      return await CommandsExecutor.Execute(command);
    }

    public async Task ExecuteCommandAsync(ICommand command)
    {
      await CommandsExecutor.Execute(command);
    }

    public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
    {
      using var scope = TenantManagementCompositionRoot.BeginLifetimeScope();
      var mediator = scope.Resolve<IMediator>();
      return await mediator.Send(query);
    }
  }
}
