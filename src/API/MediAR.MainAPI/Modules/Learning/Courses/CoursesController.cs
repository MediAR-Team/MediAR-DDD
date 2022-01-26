using MediAR.MainAPI.Configuration.Authorization;
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
    [HasPermission(LearningPermissions.GetAllCourses)]
    public async Task<IActionResult> GetCourses(int page = 1, int pageSize = 20)
    {
      var result = await _mediator.ExecuteQueryAsync(new GetCoursesQuery(page, pageSize));

      return Ok(result);
    }

    [HttpPost]
    [HasPermission(LearningPermissions.CreateCourse)]
    public async Task<IActionResult> CreateCourse(CreateCourseRequest request)
    {
      await _mediator.ExecuteCommandAsync(new CreateCourseCommand(request.Name, request.Description, request.BackgroundImageUrl));

      return Ok();
    }

    [HttpGet("foruser/me")]
    public async Task<IActionResult> GetCoursesForAuthenticatedUser()
    {
      var result = await _mediator.ExecuteQueryAsync(new GetCoursesForAuthenticatedUserQuery());

      return Ok(result);
    }
  }
}
