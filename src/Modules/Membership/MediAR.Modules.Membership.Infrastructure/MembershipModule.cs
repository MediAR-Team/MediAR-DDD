using Autofac;
using MediAR.Modules.Membership.Application.Contracts;
using MediAR.Modules.Membership.Infrastructure.Configuration;
using MediAR.Modules.Membership.Infrastructure.Configuration.Processing;
using MediatR;
using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Infrastructure
{
  public class MembershipModule : IMembershipModule
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
      using var scope = MembershipCompositionRoot.BeginLifetimeScope();
      var mediator = scope.Resolve<IMediator>();
      return await mediator.Send(query);
    }
  }
}
