using MediAR.Modules.Learning.Application.Configuration.Commands;
using MediAR.Modules.Learning.Application.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Infrastructure.Configuration.Processing
{
  class LoggingCommandHandlerWithResultDecorator<T, TResult> : ICommandHandler<T, TResult> where T : ICommand<TResult>
  {
    private readonly ILogger _logger;
    private readonly ICommandHandler<T, TResult> _decorated;

    public LoggingCommandHandlerWithResultDecorator(ILogger logger, ICommandHandler<T, TResult> decorated)
    {
      _logger = logger;
      _decorated = decorated;
    }

    public async Task<TResult> Handle(T request, CancellationToken cancellationToken)
    {
      try
      {
        _logger.LogInformation($"Executing command {request.GetType().Name}");

        var result = await _decorated.Handle(request, cancellationToken);

        _logger.LogInformation($"Successfulle executed command {request.GetType().Name}");

        return result;
      }
      catch (Exception ex)
      {
        _logger.LogError($"Processing {request.GetType().Name} failed", ex);
        throw;
      }
    }
  }
}
