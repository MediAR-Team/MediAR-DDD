using Dapper;
using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Application.FielStorage;
using MediAR.Modules.Learning.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Groups.GetCoursesForGroup
{
  internal class GetCoursesForGroupQueryHandler : IQueryHandler<GetCoursesForGroupQuery, IEnumerable<CourseDto>>
  {
    private readonly ISqlFacade _sqlFacade;
    private readonly IExecutionContextAccessor _executionContextAccessor;
    private readonly IFileStorage _fileStorage;

    public GetCoursesForGroupQueryHandler(ISqlFacade sqlFacade, IExecutionContextAccessor executionContextAccessor, IFileStorage fileStorage)
    {
      _sqlFacade = sqlFacade;
      _executionContextAccessor = executionContextAccessor;
      _fileStorage = fileStorage;
    }

    public async Task<IEnumerable<CourseDto>> Handle(GetCoursesForGroupQuery request, CancellationToken cancellationToken)
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

      var coursesWithoutImage = await _sqlFacade.QueryAsync<CourseDto>(sql, queryParams);

      return await Task.WhenAll(coursesWithoutImage.Select(async x => {
        return new CourseDto {
          Name = x.Name,
          Id = x.Id,
          Description = x.Description,
          BackgroundImageUrl = await _fileStorage.GetUrlAsync(_executionContextAccessor.TenantId.ToString(), x.BackgroundImageUrl, TimeSpan.FromMinutes(30))
        };
      }));
    }
  }
}
