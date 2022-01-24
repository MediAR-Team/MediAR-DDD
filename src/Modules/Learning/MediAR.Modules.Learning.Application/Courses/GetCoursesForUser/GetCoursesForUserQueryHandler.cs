using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Learning.Application.Configuration.Queries;
using MediAR.Modules.Learning.Application.Groups.GetCoursesForGroup;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Courses.GetCoursesForUser
{
  internal class GetCoursesForUserQueryHandler : IQueryHandler<GetCoursesForUserQuery, List<CourseDto>>
  {
    private readonly ISqlFacade _sqlFacade;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public GetCoursesForUserQueryHandler(ISqlFacade sqlFacade, IExecutionContextAccessor executionContextAccessor)
    {
      _sqlFacade = sqlFacade;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<List<CourseDto>> Handle(GetCoursesForUserQuery request, CancellationToken cancellationToken)
    {
      var sql = @"SELECT
                  SC.CourseId AS Id,
                  SC.CourseName AS [Name],
                  SC.CourseDescription AS [Description],
                  SC.BackgroundImageUrl AS BackgroundImageUrl
                  FROM [learning].[v_StudentCourses] SC
                  WHERE TenantId = @TenantId\n";

      var userFilter = string.Empty;

      switch (request.Option)
      {
        case UserIdentifierOption.UserName: userFilter = "AND UserName = @UserIdentifier"; break;
        case UserIdentifierOption.Email: userFilter = "AND Email = @UserIdentifier"; break;
      }

      sql += userFilter;

      var result = await _sqlFacade.QueryAsync<CourseDto>(sql, new { request.UserIdentifier, _executionContextAccessor.TenantId });

      return result.ToList();
    }
  }
}
