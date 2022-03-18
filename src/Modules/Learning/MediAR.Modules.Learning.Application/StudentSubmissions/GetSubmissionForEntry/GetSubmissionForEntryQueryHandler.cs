using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Learning.Application.Configuration.Queries;
using MediAR.Modules.Learning.Application.StudentSubmissions.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.StudentSubmissions.GetSubmissionForEntry
{
  internal class GetSubmissionForEntryQueryHandler : IQueryHandler<GetSubmissionForEntryQuery, StudentSubmissionDto>
  {
    private readonly IStudentSubmissionsRepository _repository;
    private readonly SubmissionDataMapperFactory _mapperFactory;

    public GetSubmissionForEntryQueryHandler(IStudentSubmissionsRepository repository, SubmissionDataMapperFactory mapperFactory)
    {
      _repository = repository;
      _mapperFactory = mapperFactory;
    }

    public async Task<StudentSubmissionDto> Handle(GetSubmissionForEntryQuery request, CancellationToken cancellationToken)
    {
      var submission = await _repository.GetForEntryAsync(request.EntryId);
      var mapper = _mapperFactory.GetMapper(submission.TypeId);
      var data = await mapper.MapDataAsync(submission.Data);
      return new StudentSubmissionDto
      {
        EntryId = request.EntryId,
        CreatedAt = submission.CreatedAt,
        ChangedAt = submission.ChangedAt,
        Data = data
      };
    }
  }
}
