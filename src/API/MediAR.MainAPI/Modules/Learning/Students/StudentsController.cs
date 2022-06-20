using MediAR.Modules.Learning.Application.Contracts;
using MediAR.Modules.Learning.Application.Students;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediAR.MainAPI.Modules.Learning.Students
{
  [ApiController]
  [Route("api/learning/[controller]")]
  public class StudentsController : ControllerBase {
    private readonly ILearningModule _mediator;

    public StudentsController(ILearningModule mediator)
    {
      _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetStudents([FromQuery] StudentsQueryModel queryParams) {
      var query = new GetStudentsQuery(queryParams.FirstName, queryParams.LastName, queryParams.UserName);

      return Ok(await _mediator.ExecuteQueryAsync(query));
    }
  }
}