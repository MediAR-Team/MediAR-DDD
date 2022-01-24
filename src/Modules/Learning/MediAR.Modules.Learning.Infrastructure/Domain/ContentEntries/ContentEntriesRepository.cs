using Dapper;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Domain;
using MediAR.Modules.Learning.Application.ContentEntries;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MediAR.Modules.Learning.Infrastructure.Domain.ContentEntries
{
  class ContentEntriesRepository : IContentEntriesRepository
  {
    private readonly ISqlFacade _sqlFacade;

    public ContentEntriesRepository(ISqlFacade sqlFacade)
    {
      _sqlFacade = sqlFacade;
    }

    public async Task SaveEntryAsync<TData, TConfig>(IContentEntry<TData, TConfig> entry)
      where TData : IXmlSerializable
      where TConfig : IXmlSerializable
    {
      var dataSerializer = new XmlSerializer(typeof(TData));
      var configSerializer = new XmlSerializer(typeof(TConfig));
      var data = string.Empty;
      var config = string.Empty;
      using (var textWriter = new StringWriter())
      {
        dataSerializer.Serialize(textWriter, entry.Data);
        data = textWriter.ToString();
      }

      using (var textWriter = new StringWriter())
      {
        configSerializer.Serialize(textWriter, entry.Configuration);
        config = textWriter.ToString();
      }

      var queryParams = new
      {
        entry.TenantId,
        entry.ModuleId,
        entry.TypeId,
        Data = data,
        Config = config
      };

      await _sqlFacade.ExecuteAsync("[learning].[ins_ContentEntry]", queryParams, commandType: CommandType.StoredProcedure);
    }

    public async Task UpdateEntryAsync<TData, TConfig>(IContentEntry<TData, TConfig> entry)
      where TData : IXmlSerializable
      where TConfig : IXmlSerializable
    {
      var dataSerializer = new XmlSerializer(typeof(TData));
      var configSerializer = new XmlSerializer(typeof(TConfig));
      var data = string.Empty;
      var config = string.Empty;
      using (var textWriter = new StringWriter())
      {
        dataSerializer.Serialize(textWriter, entry.Data);
        data = textWriter.ToString();
      }

      using (var textWriter = new StringWriter())
      {
        configSerializer.Serialize(textWriter, entry.Configuration);
        config = textWriter.ToString();
      }

      var queryParams = new
      {
        entry.Id,
        entry.TenantId,
        Data = data,
        Config = config,
        entry.TypeId
      };

      try
      {
        await _sqlFacade.ExecuteAsync("[learning].[upd_ContentEntry]", queryParams, commandType: CommandType.StoredProcedure);
      }
      catch (SqlException ex)
      {
        throw new BusinessRuleValidationException(ex.Message);
      }
    }
  }
}
