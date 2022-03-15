using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Domain;
using MediAR.Coreplatform.Infrastructure.Data;
using MediAR.Modules.Learning.Application.StudentSubmissions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MediAR.Modules.Learning.Infrastructure.Domain.StudentSubmissions
{
  class StudentSubmissionsRepository : IStudentSubmissionsRepository
  {
    private readonly ISqlFacade _sqlFacade;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public StudentSubmissionsRepository(ISqlFacade sqlFacade, IExecutionContextAccessor executionContextAccessor)
    {
      _sqlFacade = sqlFacade;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<DbStudentSubmission> GetForEntryAsync(int contentEntryId)
    {
      const string sql = @"SELECT
                          ss.Id AS Id,
                          ss.EntryId AS EntryId,
                          ss.StudentId AS StudentId,
                          ss.TenantId AS TenantId,
                          ss.[Data] AS [Data],
                          ss.CreatedAt AS CreatedAt,
                          ss.ChangedAt AS ChangedAt,
                          ss.TypeId AS TypeId
                          FROM learning.v_StudentSubmissions ss
                          WHERE TenantId = @TenantId
                          AND StudentId = @UserId
                          AND EntryId = @EntryId";

      var queryParams = new
      {
        _executionContextAccessor.TenantId,
        _executionContextAccessor.UserId,
        EntryId = contentEntryId
      };

      return await _sqlFacade.QueryFirstOrDefaultAsync<DbStudentSubmission>(sql, queryParams);
    }

    public async Task SaveAsync<TData>(IStudentSubmission<TData> submission) where TData: class, IXmlSerializable
    {
      var dataSerializer = new XmlSerializer(typeof(TData));
      var data = string.Empty;
      using (var textWriter = new StringWriter())
      {
        dataSerializer.Serialize(textWriter, submission.Data);
        data = textWriter.ToString();
      }

      var queryParams = new
      {
        _executionContextAccessor.TenantId,
        StudentId = _executionContextAccessor.UserId,
        submission.EntryId,
        Data = data,
        submission.ContentEntryType
      };

      try
      {
        await _sqlFacade.ExecuteAsync("learning.ups_StudentSubmission", queryParams, CommandType.StoredProcedure);
      }
      catch (SqlException ex)
      {
        if (ex.Number == SqlConstants.UserDefinedExceptionCode)
        {
          throw new BusinessRuleValidationException(ex.Message);
        }
        throw;
      }
    }
  }
}
