using FluentValidation;
using MediAR.Modules.Membership.Application.Contracts;
using System.Collections.Generic;

namespace MediAR.Modules.Membership.Application.Users.CreateUser
{
  public class CreateUserCommand : CommandBase<CreateUserCommandResult>
  {
    public string UserName { get; }
    public string Email { get; }
    public string Password { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string InitialRole { get; set; }

    public CreateUserCommand(string userName, string email, string password, string firstName, string lastName, string initialRole)
    {
      UserName = userName;
      Email = email;
      Password = password;
      FirstName = firstName;
      LastName = lastName;
      InitialRole = initialRole ?? "Student";
    }
  }

  public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
  {
    private static readonly List<string> ValidRoles = new() { "Student", "Instructor", "Admin" };

    public CreateUserCommandValidator() : base()
    {
      RuleFor(x => x.UserName).NotEmpty().WithMessage("User name cannot be empty");
      RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email cannot be empty");
      RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name cannot be empty");
      RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name cannot be empty");

      RuleFor(x => x.InitialRole).Must(r => ValidRoles.Contains(r)).WithMessage("Invalid role");
    }
  }
}
