using MediAR.Coreplatform.Infrastructure.CommandProcessing;
using MediAR.Modules.Membership.Application.Configuration.Commands;
using MediAR.Modules.Membership.Application.Contracts;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Infrastructure.Configuration.Processing
{
  class LoggingCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
  {
    private readonly ILogger _logger;
    private readonly ICommandHandler<T> _decorated;

    public LoggingCommandHandlerDecorator(ILogger logger, ICommandHandler<T> decorated)
    {
      _logger = logger;
      _decorated = decorated;
    }

    public async Task<Unit> Handle(T request, CancellationToken cancellationToken)
    {
      try
      {
        if (request is IUnloggedCommand)
        {
          return await _decorated.Handle(request, cancellationToken);
        }

        _logger.Information($"Executing command {request.GetType().Name}");

        var result = await _decorated.Handle(request, cancellationToken);

        _logger.Information($"Command {request.GetType().Name} executed successfully");

        return result;
      }
      catch (Exception ex)
      {
        _logger.Error($"Processing {request.GetType().Name} failed", ex);
        throw;
      }
    }
  }
}
