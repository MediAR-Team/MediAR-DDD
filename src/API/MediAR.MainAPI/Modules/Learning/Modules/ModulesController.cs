using MediAR.Modules.Learning.Application.Contracts;
using MediAR.Modules.Learning.Application.Modules.CreateModule;
using MediAR.Modules.Learning.Application.Modules.DeleteModule;
using MediAR.Modules.Learning.Application.Modules.ReorderModules;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MediAR.MainAPI.Modules.Learning.Modules
{
  [ApiController]
  [Route("api/learning/[controller]")]
  public class ModulesController : ControllerBase
  {
    private readonly ILearningModule _mediator;

    public ModulesController(ILearningModule mediator)
    {
      _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateModule(CreateModuleRequest request)
    {
      await _mediator.ExecuteCommandAsync(new CreateModuleCommand(request.Name, request.CourseId));

      return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteModule(int id)
    {
      await _mediator.ExecuteCommandAsync(new DeleteModuleCommand(id));

      return Ok();
    }

    [HttpPost("reorder")]
    public async Task<IActionResult> Reorder(ReorderRequest request)
    {
      var newOrder = request.NewOrder.Select(x => new OrderEntry(x.Id, x.Ordinal));
      var command = new ReorderModulesCommand(newOrder);

      await _mediator.ExecuteCommandAsync(command);

      return Ok();
    }
  }
}
