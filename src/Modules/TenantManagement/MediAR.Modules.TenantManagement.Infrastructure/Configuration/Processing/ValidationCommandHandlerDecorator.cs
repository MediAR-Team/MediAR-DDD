using FluentValidation;
using MediAR.Coreplatform.Infrastructure.CommandProcessing;
using MediAR.Modules.TenantManagement.Application.Configuration.Commands;
using MediAR.Modules.TenantManagement.Application.Contracts;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.TenantManagement.Infrastructure.Configuration.Processing
{
  internal class ValidationCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
  {
    private readonly ICommandHandler<T> _decorated;
    private readonly IEnumerable<IValidator<T>> _validators;

    public ValidationCommandHandlerDecorator(ICommandHandler<T> decorated, IEnumerable<IValidator<T>> validators)
    {
      _decorated = decorated;
      _validators = validators;
    }

    public async Task<Unit> Handle(T request, CancellationToken cancellationToken)
    {
      if (request is IUnloggedCommand)
      {
        return await _decorated.Handle(request, cancellationToken);
      }

      var validationErrors = _validators.SelectMany(v => v.Validate(request).Errors);
      if (validationErrors.Any())
      {
        throw new ValidationException(validationErrors);

      }

      return await _decorated.Handle(request, cancellationToken);
    }
  }

  internal class ValidationCommandHandlerDecorator<T, TResult> : ICommandHandler<T, TResult> where T : ICommand<TResult>
  {
    private readonly ICommandHandler<T, TResult> _decorated;
    private readonly IEnumerable<IValidator<T>> _validators;

    public ValidationCommandHandlerDecorator(ICommandHandler<T, TResult> decorated, IEnumerable<IValidator<T>> validators)
    {
      _decorated = decorated;
      _validators = validators;
    }

    public async Task<TResult> Handle(T request, CancellationToken cancellationToken)
    {
      if (request is IUnloggedCommand)
      {
        return await _decorated.Handle(request, cancellationToken);
      }

      var validationErrors = _validators.SelectMany(v => v.Validate(request).Errors);
      if (validationErrors.Any())
      {
        throw new ValidationException(validationErrors);

      }

      return await _decorated.Handle(request, cancellationToken);
    }
  }
}
