using Dapper;
using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Domain;
using MediAR.Modules.Learning.Application.Configuration.Commands;
using MediatR;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Courses.DeleteCourse
{
  class DeleteCourseCommandHandler : ICommandHandler<DeleteCourseCommand>
  {
    private readonly ISqlFacade _sqlFacade;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public DeleteCourseCommandHandler(ISqlFacade sqlFacade, IExecutionContextAccessor executionContextAccessor)
    {
      _sqlFacade = sqlFacade;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
      var queryParams = new
      {
        request.CourseId,
        TenantId = request.TenantId ?? _executionContextAccessor.TenantId
      };

      try
      {
        await _sqlFacade.ExecuteAsync("[learning].[del_Course]", queryParams, commandType: CommandType.StoredProcedure);
      }
      catch (SqlException ex)
      {
        throw new BusinessRuleValidationException(ex.Message);
      }

      return Unit.Value;
    }
  }
}
