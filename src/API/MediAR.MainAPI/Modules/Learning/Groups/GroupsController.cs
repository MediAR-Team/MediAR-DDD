using MediAR.Modules.Learning.Application.Groups.AddStudentToGroup;
using MediAR.Modules.Learning.Application.Groups.CreateGroup;
using MediAR.Modules.Learning.Application.Groups.DeleteGroup;
using MediAR.Modules.Learning.Application.Groups.GetGroupMembers;
using MediAR.Modules.Learning.Application.Groups.GetGroups;
using MediAR.Modules.Learning.Application.Groups.GetGroupsForAuthenticatedUser;
using MediAR.Modules.Learning.Application.Groups.GetGroupsForStudent;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediAR.MainAPI.Modules.Learning.Groups
{
  [ApiController]
  [Route("api/learning/[controller]")]
  public class GroupsController : ControllerBase
  {
    private readonly IMediator _mediator;

    public GroupsController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetGroups([FromQuery] int pageSize, [FromQuery] int page)
    {
      var groups = await _mediator.Send(new GetGroupsQuery(page, pageSize));

      return Ok(groups);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGroup(CreateGroupRequest request)
    {
      var result = await _mediator.Send(new CreateGroupCommand(request.Name));
      // TODO: add group by id endpoint and return created at
      return Ok(result);
    }

    [HttpDelete("groupId")]
    public async Task<IActionResult> DeleteGroup(int groupId)
    {
      await _mediator.Send(new DeleteGroupCommand(groupId));

      return Ok();
    }

    [HttpGet("{groupId}/members")]
    public async Task<IActionResult> GetGroupMembers(int groupId)
    {
      var members = await _mediator.Send(new GetGroupMembersQuery(groupId));

      return Ok(members);
    }

    [HttpPost("{groupId}/members")]
    public async Task<IActionResult> AddMemberToGroup(int groupId, AddMemberToGroupRequest request)
    {
      var members = await _mediator.Send(new AddStudentToGroupCommand(groupId, request.StudentId));

      return Ok(members);
    }

    [HttpGet("foruser/{userIdentifier}")]
    public async Task<IActionResult> GetGroupsForUser(string userIdentifier)
    {
      var result = await _mediator.Send(new GetGroupsForStudentQuery(userIdentifier));

      return Ok(result);
    }

    [HttpGet("foruser/me")]
    public async Task<IActionResult> GetGroupsForAuthenticatedUser()
    {
      var result = await _mediator.Send(new GetGroupsForAuthenticatedUserQuery());

      return Ok(result);
    }
  }
}
