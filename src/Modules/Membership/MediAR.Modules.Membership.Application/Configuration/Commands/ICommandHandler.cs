using MediAR.Modules.Membership.Application.Contracts;
using MediatR;

namespace MediAR.Modules.Membership.Application.Configuration.Commands
{
  public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult> { }

  public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : ICommand { }
}
