using Autofac;
using MediAR.Modules.Learning.Application.Contracts;
using MediAR.Modules.Learning.Infrastructure.Configuration;
using MediAR.Modules.Learning.Infrastructure.Configuration.Processing;
using MediatR;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Infrastructure
{
  public class LearningModule : ILearningModule
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
      using var scope = LearningCompositionRoot.BeginLifetimeScope();
      var mediator = scope.Resolve<IMediator>();
      return await mediator.Send(query);
    }
  }
}
