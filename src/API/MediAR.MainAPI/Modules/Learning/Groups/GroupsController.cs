using MediAR.MainAPI.Configuration.Authorization;
using MediAR.Modules.Learning.Application.Contracts;
using MediAR.Modules.Learning.Application.Groups.AddGroupToCourse;
using MediAR.Modules.Learning.Application.Groups.AddStudentToGroup;
using MediAR.Modules.Learning.Application.Groups.CreateGroup;
using MediAR.Modules.Learning.Application.Groups.DeleteGroup;
using MediAR.Modules.Learning.Application.Groups.GetCoursesForGroup;
using MediAR.Modules.Learning.Application.Groups.GetGroupMembers;
using MediAR.Modules.Learning.Application.Groups.GetGroups;
using MediAR.Modules.Learning.Application.Groups.GetGroupsForAuthenticatedUser;
using MediAR.Modules.Learning.Application.Groups.GetGroupsForStudent;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediAR.MainAPI.Modules.Learning.Groups
{
  [ApiController]
  [Route("api/learning/[controller]")]
  public class GroupsController : ControllerBase
  {
    private readonly ILearningModule _mediator;

    public GroupsController(ILearningModule mediator)
    {
      _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetGroups([FromQuery] int pageSize = 20, [FromQuery] int page = 1)
    {
      var groups = await _mediator.ExecuteQueryAsync(new GetGroupsQuery(page, pageSize));

      return Ok(groups);
    }

    [HttpPost]
    [HasPermission(LearningPermissions.CreateGroup)]
    public async Task<IActionResult> CreateGroup(CreateGroupRequest request)
    {
      var result = await _mediator.ExecuteCommandAsync(new CreateGroupCommand(request.Name));
      // TODO: add group by id endpoint and return created at
      return Ok(result);
    }

    [HttpDelete("groupId")]
    [HasPermission(LearningPermissions.DeleteGroup)]
    public async Task<IActionResult> DeleteGroup(int groupId)
    {
      await _mediator.ExecuteCommandAsync(new DeleteGroupCommand(groupId));

      return Ok();
    }

    [HttpGet("{groupId}/members")]
    public async Task<IActionResult> GetGroupMembers(int groupId)
    {
      var members = await _mediator.ExecuteQueryAsync(new GetGroupMembersQuery(groupId));

      return Ok(members);
    }

    [HttpPost("{groupId}/members")]
    [HasPermission(LearningPermissions.AddStudentToGoup)]
    public async Task<IActionResult> AddMemberToGroup(int groupId, AddMemberToGroupRequest request)
    {
      await _mediator.ExecuteCommandAsync(new AddStudentToGroupCommand(groupId, request.StudentId));

      return Ok();
    }

    [HttpGet("foruser/me")]
    public async Task<IActionResult> GetGroupsForAuthenticatedUser()
    {
      var result = await _mediator.ExecuteQueryAsync(new GetGroupsForAuthenticatedUserQuery());

      return Ok(result);
    }

    [HttpPost("{groupId}/courses")]
    [HasPermission(LearningPermissions.AddGroupToCourse)]
    public async Task<IActionResult> AddGroupToCourse(int groupId, AddGroupToCourseRequest request)
    {
      await _mediator.ExecuteCommandAsync(new AddGroupToCourseCommand(groupId, request.CourseId));

      return Ok();
    }

    [HttpGet("{groupId}/courses")]
    public async Task<IActionResult> GetGroupCourses(int groupId)
    {
      var result = await _mediator.ExecuteQueryAsync(new GetCoursesForGroupQuery(groupId));

      return Ok(result);
    }
  }
}
