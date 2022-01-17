using MediAR.Modules.Learning.Application.Contracts;
using MediAR.Modules.Learning.Application.Courses.CreateCourse;
using MediAR.Modules.Learning.Application.Courses.GetCourse;
using MediAR.Modules.Learning.Application.Courses.GetCourses;
using MediAR.Modules.Learning.Application.Courses.GetCoursesForAuthenticatedUser;
using MediAR.Modules.Learning.Application.Courses.GetCoursesForUser;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediAR.MainAPI.Modules.Learning.Courses
{
  [ApiController]
  [Route("api/learning/[controller]")]
  public class CoursesController : ControllerBase
  {
    private readonly ILearningModule _mediator;

    public CoursesController(ILearningModule mediator)
    {
      _mediator = mediator;
    }

    [HttpGet("{courseId}")]
    public async Task<IActionResult> GetCourse(int courseId)
    {
      var result = await _mediator.ExecuteQueryAsync(new GetCourseQuery(courseId));

      return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses(int page = 1, int pageSize = 20)
    {
      var result = await _mediator.ExecuteQueryAsync(new GetCoursesQuery(page, pageSize));

      return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse(CreateCourseRequest request)
    {
      var result = await _mediator.ExecuteCommandAsync(new CreateCourseCommand(request.Name, request.Description, request.BackgroundImageUrl));

      return Ok(result);
    }

    [HttpGet("foruser/{identifier}")]
    public async Task<IActionResult> GetCoursesForUser(string identifier, [FromQuery] UserIdentifierOption identifierOption = UserIdentifierOption.UserName)
    {
      var result = await _mediator.ExecuteQueryAsync(new GetCoursesForUserQuery(identifier, identifierOption));

      return Ok(result);
    }

    [HttpGet("forme")]
    public async Task<IActionResult> GetCoursesForAuthenticatedUser()
    {
      var result = await _mediator.ExecuteQueryAsync(new GetCoursesForAuthenticatedUserQuery());

      return Ok(result);
    }
  }
}
