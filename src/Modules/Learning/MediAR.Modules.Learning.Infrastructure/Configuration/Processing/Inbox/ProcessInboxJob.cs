using Quartz;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Infrastructure.Configuration.Processing.Inbox
{
  [DisallowConcurrentExecution]
  public class ProcessInboxJob : IJob
  {
    public async Task Execute(IJobExecutionContext context)
    {
      await CommandsExecutor.Execute(new ProcessInboxCommand());
    }
  }
}
