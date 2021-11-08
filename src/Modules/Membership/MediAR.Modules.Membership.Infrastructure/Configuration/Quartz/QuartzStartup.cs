using MediAR.Modules.Membership.Infrastructure.Configuration.Processing.Inbox;
using MediAR.Modules.Membership.Infrastructure.Configuration.Processing.InternalCommands;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;

namespace MediAR.Modules.Membership.Infrastructure.Configuration.Quartz
{
  internal static class QuartzStartup
  {
    private static IScheduler _scheduler;

    internal static void Initialize()
    {
      var schedulerConfiguration = new NameValueCollection();
      schedulerConfiguration.Add("quartz.scheduler.instanceName", "Administration");

      ISchedulerFactory schedulerFactory = new StdSchedulerFactory(schedulerConfiguration);
      _scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

      _scheduler.Start().GetAwaiter().GetResult();

      var processInboxJob = JobBuilder.Create<ProcessInboxJob>().Build();
      var processInboxTrigger =
          TriggerBuilder
              .Create()
              .StartNow()
              .WithCronSchedule("0/2 * * ? * *")
              .Build();

      _scheduler
          .ScheduleJob(processInboxJob, processInboxTrigger)
          .GetAwaiter().GetResult();

      var processInternalCommandsJob = JobBuilder.Create<ProcessInternalCommandsJob>().Build();
      var triggerCommandsProcessing =
          TriggerBuilder
              .Create()
              .StartNow()
              .WithCronSchedule("0/2 * * ? * *")
              .Build();
      _scheduler.ScheduleJob(processInternalCommandsJob, triggerCommandsProcessing).GetAwaiter().GetResult();
    }


  }
}
