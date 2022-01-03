using MediAR.Modules.Learning.Application.Configuration.Queries;
using MediAR.Modules.Learning.Application.ContentEntries.TypeHandlers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.ContentEntries.EntryTypeActions
{
  class EntryTypeActionsQueryHandler : IQueryHandler<EntryTypeActionsQuery, List<string>>
  {
    private readonly IContentEntryHandlerFactory _entryHandlerFactory;

    public EntryTypeActionsQueryHandler(IContentEntryHandlerFactory entryHandlerFactory)
    {
      _entryHandlerFactory = entryHandlerFactory;
    }

    public async Task<List<string>> Handle(EntryTypeActionsQuery request, CancellationToken cancellationToken)
    {
      var handler = await _entryHandlerFactory.GetHandlerAsync(request.EntryTypeName);

      var actionNames = handler.GetType().GetMethods().SelectMany(x => x.GetCustomAttributes(typeof(ContentEntryActionAttribute), false)).Select(attr => ((ContentEntryActionAttribute)attr).ActionName);

      return actionNames.ToList();
    }
  }
}
