using Dapper;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Membership.Domain.Users;
using System.Data;
using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Infrastructure.Domain
{
  class UsersCounter : IUsersCounter
  {
    private readonly ISqlConnectionFactory _connectionFactory;

    public UsersCounter(ISqlConnectionFactory connectionFactory)
    {
      _connectionFactory = connectionFactory;
    }

    public async Task<int> CountUsersWithUserNameOrEmailAsync(string userName, string email)
    {
      var connection = _connectionFactory.GetOpenConnection();

      var queryParams = new DynamicParameters();
      queryParams.Add("@UserName", userName, DbType.String, ParameterDirection.Input);
      queryParams.Add("@Email", email, DbType.String, ParameterDirection.Input);
      queryParams.Add("@ReturnVal", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

      await connection.QueryAsync("[membership].[count_Users_With_UserName_or_Email]", queryParams, commandType: CommandType.StoredProcedure);

      var count = queryParams.Get<int>("@ReturnVal");

      return count;
    }
  }
}
