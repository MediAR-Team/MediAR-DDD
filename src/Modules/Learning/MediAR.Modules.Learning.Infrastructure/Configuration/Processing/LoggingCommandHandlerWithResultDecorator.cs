using MediAR.Coreplatform.Domain;
using MediAR.Modules.Learning.Application.Configuration.Commands;
using MediAR.Modules.Learning.Application.Contracts;
using Serilog;
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
        _logger.Information($"Executing command {request.GetType().Name}");

        var result = await _decorated.Handle(request, cancellationToken);

        _logger.Information($"Successfulle executed command {request.GetType().Name}");

        return result;
      }
      catch (BusinessRuleValidationException) {
        throw;
      }
      catch (Exception ex)
      {
        _logger.Error($"Processing {request.GetType().Name} failed", ex);
        throw;
      }
    }
  }
}
