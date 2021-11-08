using Autofac;
using MediAR.Modules.Learning.Application.Contracts;
using MediatR;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Infrastructure.Configuration.Processing
{
  static class CommandsExecutor
  {
    internal static async Task Execute(ICommand command)
    {
      using var scope = LearningCompositionRoot.BeginLifetimeScope();
      var mediator = scope.Resolve<IMediator>();
      await mediator.Send(command);
    }

    internal static async Task<TResult> Execute<TResult>(ICommand<TResult> command)
    {
      using var scope = LearningCompositionRoot.BeginLifetimeScope();
      var mediator = scope.Resolve<IMediator>();
      return await mediator.Send(command);
    }
  }
}
