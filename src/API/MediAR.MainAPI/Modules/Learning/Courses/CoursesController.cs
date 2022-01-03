using MediAR.Modules.Learning.Application.Contracts;
using MediAR.Modules.Learning.Application.Courses.GetCourse;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
  }
}
