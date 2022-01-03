using Dapper;
using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Learning.Application.Configuration.Commands;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Courses.CreateCourse
{
  class CreateCourseCommandHandler : ICommandHandler<CreateCourseCommand, CourseDto>
  {
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public CreateCourseCommandHandler(ISqlConnectionFactory connectionFactory, IExecutionContextAccessor executionContextAccessor)
    {
      _connectionFactory = connectionFactory;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<CourseDto> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();

      var queryParams = new
      {
        request.Name,
        request.Description,
        request.BackgroundImageUrl,
        TenantId = request.TenantId ?? _executionContextAccessor.UserId
      };

      var result = await connection.ExecuteScalarAsync<CourseDto>("[learning].[ins_Course]", queryParams, commandType: CommandType.StoredProcedure);

      return result;
    }
  }
}
