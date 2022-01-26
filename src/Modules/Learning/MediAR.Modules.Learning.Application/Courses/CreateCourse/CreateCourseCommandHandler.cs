using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Learning.Application.Configuration.Commands;
using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Courses.CreateCourse
{
  class CreateCourseCommandHandler : ICommandHandler<CreateCourseCommand>
  {
    private readonly ISqlFacade _sqlFacade;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public CreateCourseCommandHandler(ISqlFacade sqlFacade, IExecutionContextAccessor executionContextAccessor)
    {
      _sqlFacade = sqlFacade;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<Unit> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
      var queryParams = new
      {
        request.Name,
        request.Description,
        BackgroundImage = request.BackgroundImageUrl,
        TenantId = request.TenantId ?? _executionContextAccessor.TenantId,
        _executionContextAccessor.UserId
      };

      await _sqlFacade.ExecuteAsync("[learning].[ins_Course]", queryParams, commandType: CommandType.StoredProcedure);

      return Unit.Value;
    }
  }
}
