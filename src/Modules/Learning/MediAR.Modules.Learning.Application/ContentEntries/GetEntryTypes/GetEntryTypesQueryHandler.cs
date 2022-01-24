using Dapper;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Learning.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.ContentEntries.GetEntryTypes
{
  class GetEntryTypesQueryHandler : IQueryHandler<GetEntryTypesQuery, List<EntryTypeDto>>
  {
    private readonly ISqlFacade _sqlFacade;

    public GetEntryTypesQueryHandler(ISqlFacade sqlFacade)
    {
      _sqlFacade = sqlFacade;
    }

    public async Task<List<EntryTypeDto>> Handle(GetEntryTypesQuery request, CancellationToken cancellationToken)
    {
      const string sql = @"SELECT [Type].[Name]
                            FROM [learning].[v_EntryTypes] [Type]";

      var result = await _sqlFacade.QueryAsync<EntryTypeDto>(sql);

      return result.ToList();
    }
  }
}
