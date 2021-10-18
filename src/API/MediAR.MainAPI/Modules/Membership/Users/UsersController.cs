using MediAR.Modules.Membership.Application.Contracts;
using MediAR.Modules.Membership.Application.Users;
using MediAR.Modules.Membership.Application.Users.AssignRoleToUser;
using MediAR.Modules.Membership.Application.Users.GetAuthenticatedUser;
using MediAR.Modules.Membership.Application.Users.GetUser;
using MediAR.Modules.Membership.Application.Users.RegisterUser;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediAR.MainAPI.Modules.Membership.Users
{
  [ApiController]
  [Route("api/membership/[controller]")]
  public class UsersController : ControllerBase
  {
    private readonly IMembershipModule _membershipModule;

    public UsersController(IMembershipModule membershipModule)
    {
      _membershipModule = membershipModule;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterNewUser(RegisterUserRequest request)
    {
      var result = await _membershipModule.ExecuteCommandAsync(new RegisterUserCommand(
        request.UserName, request.Email, request.Password, request.FirstName, request.LastName));

      return Ok(result);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
      var result = await _membershipModule.ExecuteQueryAsync(new GetAuthenticatedUserQuery());

      return Ok(result);
    }

    [HttpGet("{identifier}")]
    public async Task<IActionResult> GetUser([FromQuery] UserIdentifierOption by, string identifier)
    {
      var result = await _membershipModule.ExecuteQueryAsync(new GetUserQuery(by, identifier));

      return Ok(result);
    }

    [HttpPost("assignRole")]
    public async Task<IActionResult> AssignRole(AssignRoleToUserRequest request)
    {
      var result = await _membershipModule.ExecuteCommandAsync(new AssignRoleToUserCommand(request.IdentifierOption, request.UserIdentifier, request.RoleName));

      return Ok(result);
    }
  }
}
