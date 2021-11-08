using Dapper;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Learning.Application.Configuration.Commands;
using MediAR.Modules.Learning.Application.Groups.CreateGroup;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Infrastructure.Configuration.Processing.InternalCommands
{
  class ProcessInternalComandsCommandHandler : ICommandHandler<ProcessInternalComandsCommand>
  {
    private readonly ISqlConnectionFactory _connectionFactory;

    public ProcessInternalComandsCommandHandler(ISqlConnectionFactory connectionFactory)
    {
      _connectionFactory = connectionFactory;
    }

    public async Task<Unit> Handle(ProcessInternalComandsCommand request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();
      const string sql = @"SELECT
                          [Command].[Id] AS [Id],
                          [Command].[CreatedOn] AS [CreatedOn],
                          [Command].[Type] AS [Type],
                          [Command].[Data] AS [Data]
                          FROM [membership].[InternalCommands] [Command]
                          WHERE ProcessedOn IS NULL";

      var commands = await connection.QueryAsync<InternalCommandDto>(sql);

      const string completeSql = @"UPDATE [membership].[InternalCommands] SET [ProcessedOn] = GETDATE() WHERE [Id] = @Id";

      foreach (var command in commands)
      {
        try
        {
          await ProcessCommand(command);
        }
        catch (Exception ex)
        {
          // TODO: add logging in the future
        }
        await connection.ExecuteScalarAsync(completeSql, new { command.Id });
      }

      return Unit.Value;
    }

    private static async Task ProcessCommand(InternalCommandDto command)
    {
      var type = typeof(CreateGroupCommand).Assembly.GetType(command.Type);

      dynamic commandToProcess = JsonConvert.DeserializeObject(command.Data, type);

      await CommandsExecutor.Execute(commandToProcess);
    }
  }
}
