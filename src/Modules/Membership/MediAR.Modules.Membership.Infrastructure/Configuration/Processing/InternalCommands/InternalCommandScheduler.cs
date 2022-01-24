using Dapper;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Infrastructure.Serializaton;
using MediAR.Modules.Membership.Application.Contracts;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Infrastructure.Configuration.Processing.InternalCommands
{
    class InternalCommandScheduler : IInternalCommandScheduler
    {
        private readonly ISqlFacade _sqlFacade;

        public InternalCommandScheduler(ISqlFacade sqlFacade)
        {
            _sqlFacade = sqlFacade;
        }

        public async Task EnqueueAsync(ICommand command)
        {
            const string sql = @"INSERT INTO [membership].[InternalCommands] ([Id], [CreatedOn], [Type], [Data]) VALUES (@Id, GETDATE(), @Type, @Data)";

            await _sqlFacade.ExecuteAsync(sql, new
            {
                command.Id,
                Type = command.GetType().FullName,
                Data = JsonConvert.SerializeObject(command, new JsonSerializerSettings
                {
                    ContractResolver = new AllPropertiesContractResolver()
                })
            });
        }
    }
}
