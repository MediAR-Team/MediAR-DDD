using MediAR.Modules.Learning.Application.Contracts;
using MediAR.Modules.Learning.Application.StudentSubmissions.CreateSubmission;
using MediAR.Modules.Learning.Application.StudentSubmissions.GetSubmissionForEntry;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediAR.MainAPI.Modules.Learning.Submissions
{
  [ApiController]
  [Route("api/learning/[controller]")]
  public class SubmissionsController : ControllerBase
  {
    private readonly ILearningModule _module;

    public SubmissionsController(ILearningModule module)
    {
      _module = module;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubmission(CreateSubmissionRequest request)
    {
      var command = new CreateSubmissionCommand(request.EntryId, request.Payload);

      await _module.ExecuteCommandAsync(command);

      return Ok();
    }

    [HttpGet("forentry/{entryId}")]
    public async Task<IActionResult> GetForEntry(int entryId)
    {
      var query = new GetSubmissionForEntryQuery(entryId);

      return Ok(await _module.ExecuteQueryAsync(query));
    }
  }
}
