using Dapper;
using MediAR.Coreplatform.Application.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.ContentEntries.TypeHandlers
{
  public class ContentEntryHandlerFactory : IContentEntryHandlerFactory
  {
    private readonly ISqlFacade _sqlFacade;
    private readonly IContentEntryHandler[] _handlers;

    public ContentEntryHandlerFactory(ISqlFacade sqlFacade, IContentEntryHandler[] handlers)
    {
      _sqlFacade = sqlFacade;
      _handlers = handlers;
    }

    public async Task<IContentEntryHandler> GetHandlerAsync(int typeId)
    {
      const string sql = @"SELECT
                          [Type].[Name],
                          [Type].[HandlerClass]
                          FROM [learning].[v_EntryTypes] [Type]
                          WHERE [Id] = @Id";

      var entryTypeInfo = await _sqlFacade.QueryFirstOrDefaultAsync<EntryType>(sql, new { Id = typeId });

      if (entryTypeInfo is null)
      {
        throw new ApplicationException("Entry type with id could not be found");
      }

      var handler = _handlers.FirstOrDefault(x => x.GetType().FullName.Contains(entryTypeInfo.HandlerClass));

      return handler;
    }

    public async Task<IContentEntryHandler> GetHandlerAsync(string typeName)
    {
      const string sql = @"SELECT
                          [Type].[Name],
                          [Type].[HandlerClass]
                          FROM [learning].[v_EntryTypes] [Type]
                          WHERE [Name] = @Name";

      var entryTypeInfo = await _sqlFacade.QueryFirstOrDefaultAsync<EntryType>(sql, new { Name = typeName });

      if (entryTypeInfo is null)
      {
        throw new ApplicationException("Entry type with name could not be found");
      }

      var handler = _handlers.FirstOrDefault(x => x.GetType().FullName.Contains(entryTypeInfo.HandlerClass));

      return handler;
    }
  }

  class EntryType
  {
    public string Name { get; set; }
    public string HandlerClass { get; set; }
  }
}
