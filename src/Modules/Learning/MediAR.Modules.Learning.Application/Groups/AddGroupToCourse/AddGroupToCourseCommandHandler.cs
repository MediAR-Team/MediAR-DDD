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

namespace MediAR.Modules.Learning.Application.Groups.AddGroupToCourse
{
  internal class AddGroupToCourseCommandHandler : ICommandHandler<AddGroupToCourseCommand>
  {
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public AddGroupToCourseCommandHandler(ISqlConnectionFactory connectionFactory, IExecutionContextAccessor executionContextAccessor)
    {
      _connectionFactory = connectionFactory;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<Unit> Handle(AddGroupToCourseCommand request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();

      var queryParams = new
      {
        request.CourseId,
        request.GroupId,
        _executionContextAccessor.TenantId
      };

      try
      {
        await connection.ExecuteAsync("learning.add_Group_to_Course", queryParams, commandType: CommandType.StoredProcedure);
      }
      catch (SqlException ex)
      {
        if (ex.Number == 60000)
        {
          throw new BusinessRuleValidationException(ex.Message);
        }

        throw;
      }

      return Unit.Value;
    }
  }
}
