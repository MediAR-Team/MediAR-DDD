using Dapper;
using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Application.Queries;
using MediAR.Modules.Learning.Application.Configuration.Queries;
using MediAR.Modules.Learning.Application.ContentEntries;
using MediAR.Modules.Learning.Application.Courses.GetCourse;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Courses.GetCourses
{
  class GetCoursesQueryHandler : IQueryHandler<GetCoursesQuery, List<CourseDto>>
  {
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public GetCoursesQueryHandler(ISqlConnectionFactory connectionFactory, IExecutionContextAccessor executionContextAccessor)
    {
      _connectionFactory = connectionFactory;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<List<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();

      var pageData = PagedQueryHelper.GetPageData(request);
      var queryParams = new DynamicParameters();
      queryParams.Add(PagedQueryHelper.Next, pageData.Next);
      queryParams.Add(PagedQueryHelper.Offset, pageData.Offset);
      queryParams.Add("TenantId", _executionContextAccessor.TenantId);

      var sql = @"SELECT
                          [CMC].[CourseId] AS [Id],
                          [CMC].[TenantId] AS [TenantId],
                          [CMC].[CourseName] AS [Name],
                          [CMC].[CourseDescription] AS [Description],
                          [CMC].[CourseBackgroundImageUrl] AS [BackgroundImageUrl],
                          [CMC].[ModuleId] AS [Id],
                          [CMC].[ModuleName] AS [Name],
                          [CMC].[EntryId] AS [Id],
                          [CMC].[EntryTypeName] AS [TypeName],
                          [CMC].[EntryConfiguration] AS [Configuration],
                          [CMC].[EntryData] AS [Data]
                          FROM [learning].[v_CourseAggregate] AS [CMC]
                          WHERE [CMC].[TenantId] = @TenantId
                          ORDER BY [CMC].[CourseId]";

      sql = PagedQueryHelper.AppendPageStatement(sql);

      var courses = await connection.QueryAsync<CourseDto, ModuleDto, DbContentEntry, CourseDto>(sql, (c, m, ce) =>
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
            Configuration = config
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
      });

      return result.ToList();
    }
  }
}
