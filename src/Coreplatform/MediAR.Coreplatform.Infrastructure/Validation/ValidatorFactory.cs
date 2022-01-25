using Autofac;
using FluentValidation;
using System;

namespace MediAR.Coreplatform.Infrastructure.Validation
{
  public class ValidatorFactory : IValidatorFactory
  {
    private readonly ILifetimeScope _lifetimeScope;

    public ValidatorFactory(ILifetimeScope lifetimeScope)
    {
      _lifetimeScope = lifetimeScope;
    }

    public IValidator<T> GetValidator<T>()
    {
      var validator = _lifetimeScope.Resolve(typeof(IValidator<T>));

      return (IValidator<T>) validator;
    }

    public IValidator GetValidator(Type type)
    {
      var validatorType = typeof(IValidator<>).MakeGenericType(type);
      var validator = _lifetimeScope.Resolve(validatorType);

      return (IValidator) validator;
    }
  }
}
