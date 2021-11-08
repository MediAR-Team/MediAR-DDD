using Quartz;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Infrastructure.Configuration.Processing.InternalCommands
{
  [DisallowConcurrentExecution]
  public class ProcessInternalCommandsJob : IJob
  {
    public async Task Execute(IJobExecutionContext context)
    {
      await CommandsExecutor.Execute(new ProcessInternalComandsCommand());
    }
  }
}
