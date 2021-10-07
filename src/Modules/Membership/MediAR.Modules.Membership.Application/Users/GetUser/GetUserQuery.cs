using MediAR.Modules.Membership.Application.Contracts;

namespace MediAR.Modules.Membership.Application.Users.GetUser
{
  public class GetUserQuery : QueryBase<UserDto>
  {
    public string Identifier { get; }
    public UserIdentifierOption IdentifierOption { get; }

    public GetUserQuery(UserIdentifierOption option, string identifier)
    {
      IdentifierOption = option;
      Identifier = identifier;
    }
  }
}
