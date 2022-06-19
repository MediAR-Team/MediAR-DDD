using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Domain;
using MediAR.Modules.Learning.Application.Configuration.Queries;
using MediAR.Modules.Learning.Application.StudentSubmissions.GetSubmissionForEntry;
using MediAR.Modules.Learning.Application.StudentSubmissions.Mapping;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.StudentSubmissions.GetAllSubmissionsForEntry
{
  public class GetAllSubmissionsForEntryQueryHandler : IQueryHandler<GetAllSubmissionsForEntryQuery, IEnumerable<StudentSubmissionDto>>
  {
    private readonly IExecutionContextAccessor _executionContextAccessor;
    private readonly IStudentSubmissionsRepository _repo;
    private readonly SubmissionDataMapperFactory _mapperFactory;


    public GetAllSubmissionsForEntryQueryHandler(IExecutionContextAccessor executionContextAccessor, IStudentSubmissionsRepository repo, SubmissionDataMapperFactory mapperFactory)
    {
      _executionContextAccessor = executionContextAccessor;
      _repo = repo;
      _mapperFactory = mapperFactory;
    }

    public async Task<IEnumerable<StudentSubmissionDto>> Handle(GetAllSubmissionsForEntryQuery request, CancellationToken cancellationToken)
    {
      var submissions = (await _repo.GetAllForEntryAsync(request.EntryId)).ToList();

      var mapper = _mapperFactory.GetMapper(submissions[0].TypeId);
      return await Task.WhenAll(submissions.Select(async submission =>
      {
        var data = await mapper.MapDataAsync(submission.Data);
        return new StudentSubmissionDto
        {
          Id = submission.Id,
          EntryId = request.EntryId,
          CreatedAt = submission.CreatedAt,
          ChangedAt = submission.ChangedAt,
          Data = data
        };
      }));
    }
  }
}