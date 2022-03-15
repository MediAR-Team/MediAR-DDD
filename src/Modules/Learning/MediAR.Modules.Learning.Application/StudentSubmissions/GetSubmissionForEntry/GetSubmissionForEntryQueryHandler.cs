using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Learning.Application.Configuration.Queries;
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

    public GetSubmissionForEntryQueryHandler(IStudentSubmissionsRepository repository)
    {
      _repository = repository;
    }

    public async Task<StudentSubmissionDto> Handle(GetSubmissionForEntryQuery request, CancellationToken cancellationToken)
    {
      var submission = await _repository.GetForEntryAsync(request.EntryId);
      var data = SubmissionDataMapper.MapData(submission.TypeId, submission.Data);
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
