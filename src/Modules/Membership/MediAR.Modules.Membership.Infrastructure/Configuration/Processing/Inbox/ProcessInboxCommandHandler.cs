using Dapper;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Membership.Application.Configuration.Commands;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Infrastructure.Configuration.Processing.Inbox
{
  class ProcessInboxCommandHandler : ICommandHandler<ProcessInboxCommand>
  {
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IMediator _mediator;

    public ProcessInboxCommandHandler(ISqlConnectionFactory connectionFactory, IMediator mediator)
    {
      _connectionFactory = connectionFactory;
      _mediator = mediator;
    }

    public async Task<Unit> Handle(ProcessInboxCommand command, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();
      const string sql = @"SELECT
                          [Message].[Id] AS [Id],
                          [Message].[OccuredOn] AS [OccuredOn],
                          [Message].[Data] AS [Data],
                          [Message].[Type] AS [Type]
                          FROM [membership].[InboxMessages] [Message]
                          WHERE [Message].[ProcessedOn] IS NULL";

      var messages = await connection.QueryAsync<InboxMessageDto>(sql);

      const string updateSql = @"UPDATE [membership].[InboxMessages] SET [ProcessedOn] = GETDATE() WHERE [Id] = @Id";

      foreach (var message in messages)
      {
        var messageAssembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(x => message.Type.Contains(x.GetName().Name));

        var type = messageAssembly.GetType(message.Type);
        var request = JsonConvert.DeserializeObject(message.Data, type);

        try
        {
          await _mediator.Publish((INotification)request, cancellationToken);
        }
        catch(Exception e)
        {
          Console.WriteLine(e);
          throw;
        }

        await connection.ExecuteScalarAsync(updateSql, new { message.Id });
      }

      return Unit.Value;
    }
  }
}
