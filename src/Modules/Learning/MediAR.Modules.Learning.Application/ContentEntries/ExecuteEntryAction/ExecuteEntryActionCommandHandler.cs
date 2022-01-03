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

    public ExecuteEntryActionCommandHandler(IContentEntryHandlerFactory entryHandlerFactory)
    {
      _entryHandlerFactory = entryHandlerFactory;
    }

    public async Task<dynamic> Handle(ExecuteEntryActionCommand request, CancellationToken cancellationToken)
    {
      var handler = await _entryHandlerFactory.GetHandlerAsync(request.EntryType);

      var methods = handler.GetType().GetMethods().Where(x => x.GetCustomAttributes(typeof(ContentEntryActionAttribute), false).Length == 1);

      var method = methods.FirstOrDefault(x => ((ContentEntryActionAttribute)x.GetCustomAttributes(typeof(ContentEntryActionAttribute), false).First()).ActionName == request.ActionName);

      if (method == null)
      {
        throw new ApplicationException("Action name invalid");
      }

      var paramType = method.GetParameters().FirstOrDefault().ParameterType;

      object argument = JsonConvert.DeserializeObject(request.Payload.ToString(), paramType);

      var result = await (Task<dynamic>)method.Invoke(handler, new[] { argument });

      return result;
    }
  }
}
