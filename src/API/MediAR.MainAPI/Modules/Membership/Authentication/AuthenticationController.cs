using MediAR.Modules.Membership.Application.Authentication.Authenticate;
using MediAR.Modules.Membership.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediAR.MainAPI.Modules.Membership.Authentication
{
  [ApiController]
  [Route("api/membership/[controller]")]
  public class AuthenticationController : ControllerBase
  {
    private readonly IMembershipModule _membershipModule;

    public AuthenticationController(IMembershipModule membershipModule)
    {
      _membershipModule = membershipModule;
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest request)
    {
      var result = await _membershipModule.ExecuteCommandAsync(new AuthenticateCommand(request.UserName, request.Password));

      return Ok(result);
    }
  }
}
