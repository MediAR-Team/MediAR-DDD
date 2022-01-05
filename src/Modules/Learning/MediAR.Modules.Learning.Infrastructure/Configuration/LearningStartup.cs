using Autofac;
using MediAR.Coreplatform.Application;
using MediAR.Modules.Learning.Infrastructure.Configuration.ContentEntries;
using MediAR.Modules.Learning.Infrastructure.Configuration.DataAccess;
using MediAR.Modules.Learning.Infrastructure.Configuration.EventBus;
using MediAR.Modules.Learning.Infrastructure.Configuration.Mediation;
using MediAR.Modules.Learning.Infrastructure.Configuration.Processing;
using MediAR.Modules.Learning.Infrastructure.Configuration.Quartz;
using Microsoft.Extensions.Configuration;

namespace MediAR.Modules.Learning.Infrastructure.Configuration
{
  public static class LearningStartup
  {
    private static IContainer _container;

    public static void Initialize(IConfiguration configuration, IExecutionContextAccessor executionContextAccessor)
    {
      ConfigureCompositionRoot(configuration, executionContextAccessor);

      QuartzStartup.Initialize();
    }

    private static void ConfigureCompositionRoot(IConfiguration configuration, IExecutionContextAccessor executionContextAccessor)
    {
      var containerBuilder = new ContainerBuilder();

      containerBuilder.RegisterModule(new DataAccessModule(configuration));
      containerBuilder.RegisterModule(new EventBusModule(null));
      containerBuilder.RegisterModule(new ProcessingModule());
      containerBuilder.RegisterModule(new MediatorModule());
      containerBuilder.RegisterModule(new ContentEntriesModule());

      containerBuilder.RegisterInstance(executionContextAccessor);

      _container = containerBuilder.Build();

      LearningCompositionRoot.SetContainer(_container);
    }
  }
}
