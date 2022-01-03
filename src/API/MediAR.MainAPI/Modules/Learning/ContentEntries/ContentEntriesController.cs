using MediAR.Modules.Learning.Application.ContentEntries.EntryTypeActions;
using MediAR.Modules.Learning.Application.ContentEntries.ExecuteEntryAction;
using MediAR.Modules.Learning.Application.ContentEntries.GetEntryTypes;
using MediAR.Modules.Learning.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediAR.MainAPI.Modules.Learning.ContentEntries
{
  [ApiController]
  [Route("api/learning/[controller]")]
  public class ContentEntriesController : ControllerBase
  {
    private readonly ILearningModule _learningModule;

    public ContentEntriesController(ILearningModule learningModule)
    {
      _learningModule = learningModule;
    }

    [HttpPost("action")]
    public async Task<IActionResult> PerformAction(ContentEntryActionDto request)
    {
      var result = await _learningModule.ExecuteCommandAsync(new ExecuteEntryActionCommand(request.TypeName, request.ActionName, request.Payload));

      return Ok(result);
    }

    [HttpGet("types")]
    public async Task<IActionResult> GetTypes()
    {
      var result = await _learningModule.ExecuteQueryAsync(new GetEntryTypesQuery());

      return Ok(result);
    }

    [HttpGet("types/{typeName}/actions")]
    public async Task<IActionResult> GetTypeActions(string typeName)
    {
      var result = await _learningModule.ExecuteQueryAsync(new EntryTypeActionsQuery(typeName));

      return Ok(result);
    }
  }
}
