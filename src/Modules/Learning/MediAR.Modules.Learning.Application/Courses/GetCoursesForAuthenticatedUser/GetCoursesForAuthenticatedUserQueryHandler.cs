using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Learning.Application.Configuration.Queries;
using MediAR.Modules.Learning.Application.Groups.GetCoursesForGroup;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Courses.GetCoursesForAuthenticatedUser
{
  internal class GetCoursesForAuthenticatedUserQueryHandler : IQueryHandler<GetCoursesForAuthenticatedUserQuery, List<CourseDto>>
  {
    private readonly ISqlFacade _sqlFacade;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public GetCoursesForAuthenticatedUserQueryHandler(ISqlFacade sqlFacade, IExecutionContextAccessor executionContextAccessor)
    {
      _sqlFacade = sqlFacade;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<List<CourseDto>> Handle(GetCoursesForAuthenticatedUserQuery request, CancellationToken cancellationToken)
    {
      var sql = @"SELECT
                  SC.CourseId AS Id,
                  SC.CourseName AS [Name],
                  SC.CourseDescription AS [Description],
                  SC.BackgroundImageUrl AS BackgroundImageUrl
                  FROM [learning].[v_StudentCourses] SC
                  WHERE TenantId = @TenantId
                  AND StudentId = @UserId";

      var result = await _sqlFacade.QueryAsync<CourseDto>(sql, new { _executionContextAccessor.UserId, _executionContextAccessor.TenantId });

      return result.ToList();

    }
  }
}
