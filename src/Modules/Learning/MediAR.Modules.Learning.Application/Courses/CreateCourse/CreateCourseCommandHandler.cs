using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Application.FielStorage;
using MediAR.Modules.Learning.Application.Configuration.Commands;
using MediatR;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Courses.CreateCourse
{
  class CreateCourseCommandHandler : ICommandHandler<CreateCourseCommand>
  {
    private readonly ISqlFacade _sqlFacade;
    private readonly IExecutionContextAccessor _executionContextAccessor;
    private readonly IFileStorage _fileStorage;

    public CreateCourseCommandHandler(ISqlFacade sqlFacade, IExecutionContextAccessor executionContextAccessor, IFileStorage fileStorage)
    {
      _sqlFacade = sqlFacade;
      _executionContextAccessor = executionContextAccessor;
      _fileStorage = fileStorage;
    }

    public async Task<Unit> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
      var fileName = $"{Guid.NewGuid()}.{request.FileType}";
      var queryParams = new
      {
        request.Name,
        request.Description,
        BackgroundImage = fileName,
        TenantId = request.TenantId ?? _executionContextAccessor.TenantId,
        _executionContextAccessor.UserId
      };

      await _sqlFacade.ExecuteAsync("[learning].[ins_Course]", queryParams, commandType: CommandType.StoredProcedure);

      await _fileStorage.UploadAsync(request.BackgroundImage, _executionContextAccessor.TenantId.ToString(), fileName);

      return Unit.Value;
    }
  }
}
