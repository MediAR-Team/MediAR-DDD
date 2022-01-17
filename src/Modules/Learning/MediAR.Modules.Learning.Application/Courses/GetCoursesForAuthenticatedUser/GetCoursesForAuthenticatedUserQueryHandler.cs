using Dapper;
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
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public GetCoursesForAuthenticatedUserQueryHandler(ISqlConnectionFactory connectionFactory, IExecutionContextAccessor executionContextAccessor)
    {
      _connectionFactory = connectionFactory;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<List<CourseDto>> Handle(GetCoursesForAuthenticatedUserQuery request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();

      var sql = @"SELECT
                  SC.CourseId AS Id,
                  SC.CourseName AS [Name],
                  SC.CourseDescription AS [Description],
                  SC.BackgroundImageUrl AS BackgroundImageUrl
                  FROM [learning].[v_StudentCourses] SC
                  WHERE TenantId = @TenantId
                  AND UserId = @UserId";

      var result = await connection.QueryAsync<CourseDto>(sql, new { _executionContextAccessor.UserId, _executionContextAccessor.TenantId });

      return result.ToList();

    }
  }
}
