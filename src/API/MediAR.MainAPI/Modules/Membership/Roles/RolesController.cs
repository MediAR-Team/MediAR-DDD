using MediAR.Modules.Membership.Application.Contracts;
using MediAR.Modules.Membership.Application.Roles.GetRoles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediAR.MainAPI.Modules.Membership.Roles
{
  [ApiController]
  [Route("api/membership/[controller]")]
  public class RolesController : ControllerBase
  {
    private readonly IMembershipModule _membershipModule;

    public RolesController(IMembershipModule membershipModule)
    {
      _membershipModule = membershipModule;
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles([FromQuery] GetRolesRequest request)
    {
      var result = await _membershipModule.ExecuteQueryAsync(new GetRolesQuery(request.Page, request.PageSize));

      return Ok(result);
    }
  }
}
