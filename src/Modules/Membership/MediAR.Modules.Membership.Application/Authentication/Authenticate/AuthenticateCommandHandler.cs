using Dapper;
using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Membership.Application.Authentication.TokenProviding;
using MediAR.Modules.Membership.Application.Configuration.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Application.Authentication.Authenticate
{
  class AuthenticateCommandHandler : ICommandHandler<AuthenticateCommand, AuthenticationResult>
  {
    private const string InvalidUserNameOrPassswordMessage = "UserName or password invalid";

    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly ITokenProvider _tokenProvider;

    public AuthenticateCommandHandler(ISqlConnectionFactory connectionFactory, ITokenProvider tokenProvider)
    {
      _connectionFactory = connectionFactory;
      _tokenProvider = tokenProvider;
    }

    public async Task<AuthenticationResult> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();

      const string sql = @"SELECT
                            [U].[Id],
                            [U].[UserName],
                            [U].[PasswordHash],
                            [U].[Email],
                            [U].[FirstName],
                            [U].[LastName],
                            [U].[TenantId]
                          FROM [membership].[v_Users] [U]
                          WHERE UserName = @UserName";

      var user = await connection.QuerySingleOrDefaultAsync<UserDto>(sql, new { UserName = request.UserName });

      if (user == null || !PasswordManager.VerifyHashedPassword(user.PasswordHash, request.Password))
      {
        return new AuthenticationResult(InvalidUserNameOrPassswordMessage);
      }

      return new AuthenticationResult(_tokenProvider.GenerateToken(user), "");
    }
  }
}
