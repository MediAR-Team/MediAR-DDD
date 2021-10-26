﻿using Dapper;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Membership.Application.Configuration.Commands;
using MediAR.Modules.Membership.Application.Users.CreateUser;
using MediatR;
using Newtonsoft.Json;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Infrastructure.Configuration.Processing.InternalCommands
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
                          [Command].[OccuredOn] AS [OccuredOn],
                          [Command].[Type] AS [Type],
                          [Command].[Data] AS [Data]
                          FROM [membership].[InternalCommands] [Command]
                          WHERE ProcessedOn IS NULL";

      var commands = (await connection.QueryAsync<InternalCommandDto>(sql)).ToList();

      const string completeSql = @"UPDATE [membership].[InternalCommands] SET [ProcessedOn] = GETDATE() WHERE [Id] = @Id";

      foreach (var command in commands)
      {
        await ProcessCommand(command);
        await connection.ExecuteScalarAsync(completeSql, new { command.Id });
      }

      return Unit.Value;
    }

    private static async Task ProcessCommand(InternalCommandDto command)
    {
      var type = typeof(CreateUserCommand).Assembly.GetType(command.Type);

      dynamic commandToProcess = JsonConvert.DeserializeObject(command.Data, type);

      await CommandsExecutor.Execute(commandToProcess);
    }
  }
}
