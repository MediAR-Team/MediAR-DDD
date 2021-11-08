using Autofac;
using Dapper;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Infrastructure.EventBus;
using MediAR.Coreplatform.Infrastructure.Serializaton;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Infrastructure.Configuration.EventBus
{
  class IntegrationEventGenericHandler<T> : IIntegrationEventHandler<T> where T : IntegrationEvent
  {
    public async Task Handle(T @event)
    {
      using var scope = LearningCompositionRoot.BeginLifetimeScope();
      using var connection = scope.Resolve<ISqlConnectionFactory>().GetOpenConnection();
      string type = @event.GetType().FullName;
      var data = JsonConvert.SerializeObject(@event, new JsonSerializerSettings
      {
        ContractResolver = new AllPropertiesContractResolver()
      });

      const string sql = "INSERT INTO [learning].[InboxMessages] ([Id], [OccuredOn], [Type], [Data]) VALUES (@Id, @OccuredOn, @Type, @Data)";

      await connection.ExecuteScalarAsync(sql, new
      {
        @event.Id,
        @event.OccuredOn,
        type,
        data
      });
    }
  }
}
