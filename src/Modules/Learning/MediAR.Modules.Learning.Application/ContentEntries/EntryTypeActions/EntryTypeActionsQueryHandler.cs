using MediAR.Modules.Learning.Application.Configuration.Queries;
using MediAR.Modules.Learning.Application.ContentEntries.TypeHandlers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.ContentEntries.EntryTypeActions
{
  class EntryTypeActionsQueryHandler : IQueryHandler<EntryTypeActionsQuery, List<EntryTypeActionDto>>
  {
    private readonly IContentEntryHandlerFactory _entryHandlerFactory;

    public EntryTypeActionsQueryHandler(IContentEntryHandlerFactory entryHandlerFactory)
    {
      _entryHandlerFactory = entryHandlerFactory;
    }

    public async Task<List<EntryTypeActionDto>> Handle(EntryTypeActionsQuery request, CancellationToken cancellationToken)
    {
      var handler = await _entryHandlerFactory.GetHandlerAsync(request.EntryTypeName);

      var methods = handler.GetType().GetMethods().Where(m => m.GetCustomAttributes(typeof(ContentEntryActionAttribute), false).Length == 1);

      var result = methods.Select(m => new EntryTypeActionDto
      {
        Name = ((ContentEntryActionAttribute)m.GetCustomAttributes(typeof(ContentEntryActionAttribute), false).First()).ActionName,
        Params = m.GetParameters().First().ParameterType.GetProperties().Select(prop => new ActionParamDto
        {
          Name = prop.Name,
          DataType = prop.PropertyType.Name
        }).ToList()
      });

      return result.ToList();
    }
  }
}
