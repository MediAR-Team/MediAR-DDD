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
    private readonly ISqlConnectionFactory _connectionFactory;

    public GetEntryTypesQueryHandler(ISqlConnectionFactory connectionFactory)
    {
      _connectionFactory = connectionFactory;
    }

    public async Task<List<EntryTypeDto>> Handle(GetEntryTypesQuery request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();

      const string sql = @"SELECT [Type].[Name]
                            FROM [learning].[v_EntryTypes] [Type]";

      var result = await connection.QueryAsync<EntryTypeDto>(sql);

      return result.ToList();
    }
  }
}
