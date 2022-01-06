using MediAR.Modules.Learning.Application.Contracts;
using MediAR.Modules.Learning.Application.Courses.GetCourse;
using MediAR.Modules.Learning.Application.Courses.GetCourses;
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
  }
}
