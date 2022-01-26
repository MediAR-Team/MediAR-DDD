using FluentValidation;
using MediAR.Coreplatform.Domain;
using MediAR.Modules.Learning.Application.Configuration.Commands;
using MediAR.Modules.Learning.Application.ContentEntries.TypeHandlers;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.ContentEntries.ExecuteEntryAction
{
  class ExecuteEntryActionCommandHandler : ICommandHandler<ExecuteEntryActionCommand, object>
  {
    private readonly IContentEntryHandlerFactory _entryHandlerFactory;
    private readonly IValidatorFactory _validatorFactory;

    public ExecuteEntryActionCommandHandler(IContentEntryHandlerFactory entryHandlerFactory, IValidatorFactory validatorFactory)
    {
      _entryHandlerFactory = entryHandlerFactory;
      _validatorFactory = validatorFactory;
    }

    public async Task<dynamic> Handle(ExecuteEntryActionCommand request, CancellationToken cancellationToken)
    {
      var handler = await _entryHandlerFactory.GetHandlerAsync(request.EntryType);

      var ceActionAttributeType = typeof(ContentEntryActionAttribute);

      var methods = handler.GetType().GetMethods().Where(x => x.GetCustomAttributes(ceActionAttributeType, false).Length == 1);

      var method = methods.FirstOrDefault(x => ((ContentEntryActionAttribute)x.GetCustomAttributes(ceActionAttributeType, false).First()).ActionName == request.ActionName);

      if (method == null)
      {
        throw new BusinessRuleValidationException("Action name invalid");
      }

      var paramType = method.GetParameters().FirstOrDefault().ParameterType;

      object argument = JsonConvert.DeserializeObject(request.Payload.ToString(), paramType);

      ValidateArgument(argument, paramType);

      var result = await (Task<dynamic>)method.Invoke(handler, new[] { argument });

      return result;
    }

    private void ValidateArgument(object argument, Type paramType)
    {
      var validator = _validatorFactory.GetValidator(paramType);

      if (validator != null)
      {
        var validationErrors = validator.Validate(argument);

        if (validationErrors?.Errors?.Count != 0)
        {
          throw new ValidationException(validationErrors.Errors);
        }
      }
    }
  }
}
