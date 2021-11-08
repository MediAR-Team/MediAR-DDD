using Dapper;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Infrastructure.Serializaton;
using MediAR.Modules.Learning.Application.Contracts;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Infrastructure.Configuration.Processing.InternalCommands
{
    class InternalCommandScheduler : IInternalCommandScheduler
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public InternalCommandScheduler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task EnqueueAsync(ICommand command)
        {
            var connection = _connectionFactory.GetOpenConnection();

            const string sql = @"INSERT INTO [membership].[InternalCommands] ([Id], [CreatedOn], [Type], [Data]) VALUES (@Id, GETDATE(), @Type, @Data)";

            await connection.ExecuteScalarAsync(sql, new
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
