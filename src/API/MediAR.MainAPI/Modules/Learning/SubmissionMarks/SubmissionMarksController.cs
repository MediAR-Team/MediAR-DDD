using MediAR.Modules.Learning.Application.Contracts;
using MediAR.Modules.Learning.Application.SubmissionMarks.CreateSubmissionMark;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediAR.MainAPI.Modules.Learning.SubmissionMarks
{
  [ApiController]
  [Route("api/learning/[controller]")]
  public class SubmissionMarksController : ControllerBase
  {
    private readonly ILearningModule _mediator;

    public SubmissionMarksController(ILearningModule mediator)
    {
      _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMark(CreateSubmissionMarkRequest request)
    {
      await _mediator.ExecuteCommandAsync(new CreateSubmissionMarkCommand(request.SubmissionId, request.Mark, request.Comment));
      return Ok();
    }
  }
}
