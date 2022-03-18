using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.FielStorage;
using MediAR.Coreplatform.Domain.BaseModels;
using MediAR.Modules.Learning.Application.StudentSubmissions.SubmissionTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MediAR.Modules.Learning.Application.StudentSubmissions.Mapping
{
  internal class SubmissionTaskSubmissionDataMapper : ISubmissionDataMapper
  {
    private readonly IFileStorage _fileStorage;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public SubmissionTaskSubmissionDataMapper(IFileStorage fileStorage, IExecutionContextAccessor executionContextAccessor)
    {
      _fileStorage = fileStorage;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<object> MapDataAsync(string dataXml)
    {
      var dataSerializer = new XmlSerializer(typeof(SubmissionTaskSubmissionData));
      using var reader = new StringReader(dataXml);
      var data = (SubmissionTaskSubmissionData)dataSerializer.Deserialize(reader);
      var files = new List<FileDescriptorDto>();

      foreach (var f in data.FileNames)
      {
        files.Add(new FileDescriptorDto(await _fileStorage.GetUrlAsync(_executionContextAccessor.TenantId.ToString(), f.Name, TimeSpan.FromMinutes(30)), f.DisplayName));
      }

      return new
      {
        data.Comment,
        Files = files
      };
    }
  }
}
