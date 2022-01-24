using Dapper;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Membership.Application.Configuration.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Application.Users.GetUser
{
  class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto>
  {
    private readonly ISqlFacade _sqlFacade;

    public GetUserQueryHandler(ISqlFacade sqlFacade)
    {
      _sqlFacade = sqlFacade;
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
      var baseSql = @"SELECT
                          [User].[UserName],
                          [User].[Email],
                          [User].[FirstName],
                          [User].[LastName],
                          [User].[TenantId]
                          FROM [membership].[v_Users] [User]";

      var sql = AppendFilter(baseSql, request);

      var user = await _sqlFacade.QueryFirstOrDefaultAsync<UserDto>(sql, new { request.Identifier });

      return user;
    }

    private string AppendFilter(string sql, GetUserQuery request)
    {
      var result = sql;
      switch (request.IdentifierOption)
      {
        case UserIdentifierOption.UserName:
          result = $"{sql} WHERE UserName = @Identifier";
          break;
        case UserIdentifierOption.Email:
          result = $"{sql} WHERE Email = @Identifier";
          break;
      }

      return result;
    }
  }
}
