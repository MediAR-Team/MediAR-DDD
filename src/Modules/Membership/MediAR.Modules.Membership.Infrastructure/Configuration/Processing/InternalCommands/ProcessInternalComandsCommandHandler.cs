using Dapper;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Membership.Application.Configuration.Commands;
using MediAR.Modules.Membership.Application.Users.CreateUser;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Infrastructure.Configuration.Processing.InternalCommands
{
  class ProcessInternalComandsCommandHandler : ICommandHandler<ProcessInternalComandsCommand>
  {
    private readonly ISqlFacade _sqlFacade;

    public ProcessInternalComandsCommandHandler(ISqlFacade sqlFacade)
    {
      _sqlFacade = sqlFacade;
    }

    public async Task<Unit> Handle(ProcessInternalComandsCommand request, CancellationToken cancellationToken)
    {
      const string sql = @"SELECT
                          [Command].[Id] AS [Id],
                          [Command].[CreatedOn] AS [CreatedOn],
                          [Command].[Type] AS [Type],
                          [Command].[Data] AS [Data]
                          FROM [membership].[InternalCommands] [Command]
                          WHERE ProcessedOn IS NULL";

      var commands = await _sqlFacade.QueryAsync<InternalCommandDto>(sql);

      const string completeSql = @"UPDATE [membership].[InternalCommands] SET [ProcessedOn] = GETDATE() WHERE [Id] = @Id";

      foreach (var command in commands)
      {
        try
        {
          await ProcessCommand(command);
          await _sqlFacade.ExecuteAsync(completeSql, new { command.Id });
        }
        catch (Exception ex)
        {
          // TODO: add logging in the future
        }
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
