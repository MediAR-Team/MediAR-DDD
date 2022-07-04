using MediAR.Modules.Learning.Application.ContentEntries;
using MediAR.Modules.Learning.Application.ContentEntries.EntryTypeActions;
using MediAR.Modules.Learning.Application.ContentEntries.ExecuteEntryAction;
using MediAR.Modules.Learning.Application.ContentEntries.GetEntryTypes;
using MediAR.Modules.Learning.Application.ContentEntries.ReorderContentEntries;
using MediAR.Modules.Learning.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
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

      if (result is ActionFileResponse fileResult)
      {
        return fileResult.FileData != null ? File(fileResult.FileData, fileResult.FileType) : File(fileResult.FileStream, fileResult.FileType);
      }

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

    [HttpPost("reorder")]
    public async Task<IActionResult> Reorder(ReorderRequest request)
    {
      var newOrder = request.NewOrder.Select(x => new OrderEntry(x.Id, x.Ordinal));
      var command = new ReorderContentEntriesCommand(newOrder);

      await _learningModule.ExecuteCommandAsync(command);

      return Ok();
    }
  }
}
