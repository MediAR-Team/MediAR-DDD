using Autofac;
using MediAR.Modules.Membership.Application.Contracts;
using MediatR;
using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Infrastructure.Configuration.Processing
{
  static class CommandsExecutor
  {
    internal static async Task Execute(ICommand command)
    {
      using var scope = MembershipCompositionRoot.BeginLifetimeScope();
      var mediator = scope.Resolve<IMediator>();
      await mediator.Send(command);
    }

    internal static async Task<TResult> Execute<TResult>(ICommand<TResult> command)
    {
      using var scope = MembershipCompositionRoot.BeginLifetimeScope();
      var mediator = scope.Resolve<IMediator>();
      return await mediator.Send(command);
    }
  }
}
