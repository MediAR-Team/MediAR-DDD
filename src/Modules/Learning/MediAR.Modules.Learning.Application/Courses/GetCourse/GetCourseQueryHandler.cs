using Dapper;
using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Learning.Application.Configuration.Queries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Courses.GetCourse
{
  class GetCourseQueryHandler : IQueryHandler<GetCourseQuery, CourseDto>
  {
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public GetCourseQueryHandler(ISqlConnectionFactory connectionFactory, IExecutionContextAccessor executionContextAccessor)
    {
      _connectionFactory = connectionFactory;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<CourseDto> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();

      const string sql = @"SELECT
                          [CMC].[CourseId] AS [Id],
                          [CMC].[TenantId] AS [TenantId],
                          [CMC].[CourseName] AS [Name],
                          [CMC].[CourseDescription] AS [Description],
                          [CMC].[CourseBackgroundImageUrl] AS [BackgroundImageUrl],
                          [CMC].[ModuleId] AS [Id],
                          [CMC].[ModuleName] AS [Name],
                          [CMC].[EntryId] AS [Id],
                          [CMC].[EntryConfiguration] AS [Configuration],
                          [CMC].[EntryData] AS [Data]
                          FROM [learning].[v_CourseAggregate] AS [CMC]
                          WHERE [CMC].[TenantId] = @TenantId AND [CMC].[CourseId] = @CourseId";

      var queryParams = new
      {
        request.CourseId,
        TenantId = request.TenantId ?? _executionContextAccessor.TenantId
      };

      var courses = await connection.QueryAsync<CourseDto, ModuleDto, ContentEntryDto, CourseDto>(sql, (c, m, ce) =>
      {
        c.Modules.Add(m);
        m.ContentEntries.Add(ce);
        return c;
      }, queryParams, splitOn: "Id, Id");

      var result = courses.GroupBy(x => x.Id).Select(courseGroup =>
      {
        var groupedCourse = courseGroup.First();

        groupedCourse.Modules = courseGroup.Select(x => x.Modules.Single()).GroupBy(m => m.Id).Select(moduleGroup =>
          {
            var groupedModule = moduleGroup.First();
            groupedModule.ContentEntries = moduleGroup.Select(m => m.ContentEntries.Single()).ToList();
            return groupedModule;
          }).ToList();

        return groupedCourse;
      }).FirstOrDefault();

      return result;
    }
  }
}
