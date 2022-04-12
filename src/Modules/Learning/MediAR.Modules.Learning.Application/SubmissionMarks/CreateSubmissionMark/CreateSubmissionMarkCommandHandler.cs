using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Domain;
using MediAR.Coreplatform.Infrastructure.Data;
using MediAR.Modules.Learning.Application.Configuration.Commands;
using MediatR;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.SubmissionMarks.CreateSubmissionMark
{
  internal class CreateSubmissionMarkCommandHandler : ICommandHandler<CreateSubmissionMarkCommand>
  {
    private readonly ISqlFacade _sqlFacade;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public CreateSubmissionMarkCommandHandler(ISqlFacade sqlFacade, IExecutionContextAccessor executionContextAccessor)
    {
      _sqlFacade = sqlFacade;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<Unit> Handle(CreateSubmissionMarkCommand request, CancellationToken cancellationToken)
    {
      var queryParams = new
      {
        request.Submissionid,
        request.Mark,
        request.Comment,
        _executionContextAccessor.TenantId,
        InstructorId = _executionContextAccessor.UserId
      };

      try
      {
        await _sqlFacade.ExecuteAsync("[learning].[ins_SubmissionMark]", queryParams, commandType: CommandType.StoredProcedure);
        return Unit.Value;
      }
      catch (SqlException ex)
      {
        if (ex.Number == SqlConstants.UserDefinedExceptionCode)
        {
          throw new BusinessRuleValidationException(ex.Message);
        }
        throw;
      }

      throw new NotImplementedException();
    }
  }
}
