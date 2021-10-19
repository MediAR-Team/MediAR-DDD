using MediAR.Modules.Membership.Application.Contracts;

namespace MediAR.Modules.Membership.Application.Users.CreateUser
{
  public class CreateUserCommand : CommandBase<CreateUserCommandResult>
  {
    public string UserName { get; }
    public string Email { get; }
    public string Password { get; }
    public string FirstName { get; }
    public string LastName { get; }

    public CreateUserCommand(string userName, string email, string password, string firstName, string lastName)
    {
      UserName = userName;
      Email = email;
      Password = password;
      FirstName = firstName;
      LastName = lastName;
    }
  }
}
