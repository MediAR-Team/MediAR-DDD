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
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public GetAuthenticatedUserQueryHandler(ISqlConnectionFactory connectionFactory, IExecutionContextAccessor executionContextAccessor)
    {
      _connectionFactory = connectionFactory;
      _executionContextAccessor = executionContextAccessor;
    }

    public async Task<UserDto> Handle(GetAuthenticatedUserQuery request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();

      var userId = _executionContextAccessor.UserId;

      const string sql = @"SELECT
                          [User].[UserName],
                          [User].[Email],
                          [User].[FirstName],
                          [User].[LastName],
                          [User].[TenantId]
                          FROM [membership].[v_Users] [User]
                          WHERE Id = @UserId";

      var user = await connection.QueryFirstOrDefaultAsync<UserDto>(sql, new { UserId = userId });

      return user;
    }
  }
}
