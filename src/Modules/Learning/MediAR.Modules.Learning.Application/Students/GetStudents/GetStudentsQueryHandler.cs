using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Learning.Application.Configuration.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Students
{
  public class GetStudentsQueryHandler : IQueryHandler<GetStudentsQuery, IEnumerable<StudentDto>>
  {
    private readonly ISqlFacade _sqlFacade;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public GetStudentsQueryHandler(ISqlFacade sqlFacade, IExecutionContextAccessor executionContextAccessor)
    {
      _sqlFacade = sqlFacade;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<IEnumerable<StudentDto>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
      const string sql = @"SELECT
                            s.Id,
                            s.TenantId,
                            s.UserName,
                            s.FirstName,
                            s.LastName,
                            s.Email
                          FROM [learning].[v_Students] s
                          WHERE s.TenantId = @TenantId
                            AND (@UserName IS NULL OR s.UserName LIKE '%' + @UserName + '%')
                            AND (@FirstName IS NULL OR s.FirstName LIKE '%' + @FirstName + '%')
                            AND (@LastName IS NULL OR s.LastName LIKE '%' + @LastName + '%')
      ";
      var parameters = new {
        _executionContextAccessor.TenantId,
        FirstName = string.IsNullOrEmpty(request.FirstName) ? null : request.FirstName,
        LastName = string.IsNullOrEmpty(request.LastName) ? null : request.LastName,
        UserName = string.IsNullOrEmpty(request.UserName) ? null : request.UserName
      };

      var result = await _sqlFacade.QueryAsync<StudentDto>(sql, parameters);
      
      return result;
    }
  }
}
