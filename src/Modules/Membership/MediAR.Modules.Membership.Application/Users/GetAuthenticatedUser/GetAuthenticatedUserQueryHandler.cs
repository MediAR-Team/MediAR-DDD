using Dapper;
using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Membership.Application.Configuration.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Application.Users.GetAuthenticatedUser
{
  class GetAuthenticatedUserQueryHandler : IQueryHandler<GetAuthenticatedUserQuery, UserDto>
  {
    private readonly ISqlFacade _sqlFacade;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public GetAuthenticatedUserQueryHandler(ISqlFacade sqlFacade, IExecutionContextAccessor executionContextAccessor)
    {
      _sqlFacade = sqlFacade;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<UserDto> Handle(GetAuthenticatedUserQuery request, CancellationToken cancellationToken)
    {
      var userId = _executionContextAccessor.UserId;

      const string sql = @"SELECT
                          [User].[UserName],
                          [User].[Email],
                          [User].[FirstName],
                          [User].[LastName],
                          [User].[TenantId]
                          FROM [membership].[v_Users] [User]
                          WHERE Id = @UserId
                            AND TenantId = @TenantId";

      var user = await _sqlFacade.QueryFirstOrDefaultAsync<UserDto>(sql, new { UserId = userId, _executionContextAccessor.TenantId });

      return user;
    }
  }
}
