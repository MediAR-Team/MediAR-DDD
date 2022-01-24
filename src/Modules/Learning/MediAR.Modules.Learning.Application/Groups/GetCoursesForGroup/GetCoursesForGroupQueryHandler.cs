using Dapper;
using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Learning.Application.Configuration.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Groups.GetCoursesForGroup
{
  internal class GetCoursesForGroupQueryHandler : IQueryHandler<GetCoursesForGroupQuery, List<CourseDto>>
  {
    private readonly ISqlFacade _sqlFacade;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public GetCoursesForGroupQueryHandler(ISqlFacade sqlFacade, IExecutionContextAccessor executionContextAccessor)
    {
      _sqlFacade = sqlFacade;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<List<CourseDto>> Handle(GetCoursesForGroupQuery request, CancellationToken cancellationToken)
    {
      const string sql = @"SELECT
                          GC.CourseId AS Id,
                          GC.CourseName AS [Name],
                          GC.CourseDescription AS [Description],
                          GC.BackgroundImageUrl AS BackgroundImageUrl
                          FROM [learning].[v_GroupCourses] [GC]
                          WHERE TenantId = @TenantId
                          AND GroupId = @GroupId";

      var queryParams = new
      {
        request.GroupId,
        _executionContextAccessor.TenantId
      };

      var result = await _sqlFacade.QueryAsync<CourseDto>(sql, queryParams);

      return result.ToList();
    }
  }
}
