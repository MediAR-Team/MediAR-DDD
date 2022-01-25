using Dapper;
using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Learning.Application.Configuration.Queries;
using MediAR.Modules.Learning.Application.ContentEntries;
using MediAR.Modules.Learning.Application.Courses.GetCourse;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Courses.GetCourses
{
  class GetCourseQueryHandler : IQueryHandler<GetCourseQuery, CourseAggregateDto>
  {
    private readonly ISqlFacade _sqlFacade;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public GetCourseQueryHandler(ISqlFacade sqlFacade, IExecutionContextAccessor executionContextAccessor)
    {
      _sqlFacade = sqlFacade;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<CourseAggregateDto> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
      const string sql = @"SELECT
                          [CMC].[CourseId] AS [Id],
                          [CMC].[TenantId] AS [TenantId],
                          [CMC].[CourseName] AS [Name],
                          [CMC].[CourseDescription] AS [Description],
                          [CMC].[CourseBackgroundImageUrl] AS [BackgroundImageUrl],
                          [CMC].[ModuleId] AS [Id],
                          [CMC].[ModuleName] AS [Name],
                          [CMC].[ModuleOrdinal] AS [Ordinal],
                          [CMC].[EntryId] AS [Id],
                          [CMC].[EntryTypeName] AS [TypeName],
                          [CMC].[EntryConfiguration] AS [Configuration],
                          [CMC].[EntryData] AS [Data],
                          [CMC].[EntryOrdinal] AS [Ordinal]
                          FROM [learning].[v_CourseAggregate] AS [CMC]
                          WHERE [CMC].[TenantId] = @TenantId AND [CMC].[CourseId] = @CourseId
                          ORDER BY [CMC].[CourseId], [CMC].[ModuleOrdinal], [CMC].[EntryOrdinal]";

      var queryParams = new
      {
        request.CourseId,
        TenantId = request.TenantId ?? _executionContextAccessor.TenantId
      };

      var courses = await _sqlFacade.QueryAsync<CourseAggregateDto, ModuleDto, DbContentEntry, CourseAggregateDto>(sql, (c, m, ce) =>
      {
        if (m != null)
        {
          c.Modules.Add(m);
        }
        if (ce != null)
        {
          var (data, config) = EntryMapper.MapEntry(ce);
          var ceDto = new ContentEntryDto
          {
            Id = ce.Id,
            TypeName = ce.TypeName,
            Data = data,
            Configuration = config,
            Ordinal = ce.Ordinal
          };

          m.ContentEntries.Add(ceDto);
        }
        return c;
      }, queryParams, splitOn: "Id, Id");

      var result = courses.GroupBy(x => x.Id).Select(courseGroup =>
      {
        var groupedCourse = courseGroup.First();
        if (groupedCourse.Modules.Count != 0)
        {

          groupedCourse.Modules = courseGroup.Select(x => x.Modules.Single()).GroupBy(m => m.Id).Select(moduleGroup =>
          {
            var groupedModule = moduleGroup.First();
            if (groupedModule.ContentEntries.Count != 0)
            {
              groupedModule.ContentEntries = moduleGroup.Select(m => m.ContentEntries.Single()).ToList();
            }
            return groupedModule;
          }).ToList();

        }
        return groupedCourse;
      }).FirstOrDefault();

      return result;
    }
  }
}
