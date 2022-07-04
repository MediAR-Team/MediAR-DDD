using Autofac;
using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Infrastructure.FileStorage;
using MediAR.Coreplatform.Infrastructure.PdfConversion;
using MediAR.Modules.Learning.Infrastructure.Configuration.DataAccess;
using MediAR.Modules.Learning.Infrastructure.Configuration.Domain;
using MediAR.Modules.Learning.Infrastructure.Configuration.EventBus;
using MediAR.Modules.Learning.Infrastructure.Configuration.Mediation;
using MediAR.Modules.Learning.Infrastructure.Configuration.Processing;
using MediAR.Modules.Learning.Infrastructure.Configuration.Quartz;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Net.Http;

namespace MediAR.Modules.Learning.Infrastructure.Configuration
{
  public static class LearningStartup
  {
    private static IContainer _container;

    public static void Initialize(IConfiguration configuration, IExecutionContextAccessor executionContextAccessor, ILogger logger, IHttpClientFactory httpClientFactory)
    {
      ConfigureCompositionRoot(configuration, executionContextAccessor, logger, httpClientFactory);

      QuartzStartup.Initialize();
      EventBusStartup.Initialize();
    }

    private static void ConfigureCompositionRoot(IConfiguration configuration, IExecutionContextAccessor executionContextAccessor, ILogger logger, IHttpClientFactory httpClientFactory)
    {
      var containerBuilder = new ContainerBuilder();

      containerBuilder
        .RegisterInstance(logger.ForContext("Module", "Learning"))
        .As<ILogger>()
        .SingleInstance();

      containerBuilder.RegisterModule(new DataAccessModule(configuration));
      containerBuilder.RegisterModule(new EventBusModule(null));
      containerBuilder.RegisterModule(new ProcessingModule());
      containerBuilder.RegisterModule(new MediatorModule());
      containerBuilder.RegisterModule(new DomainModule());
      containerBuilder.RegisterModule(new FileStorageModule(configuration));

      var mdToPdfConfig = new MarkdownToPdfConfiguration();
      configuration.GetSection("mdToPdfConfig").Bind(mdToPdfConfig);
      containerBuilder.RegisterInstance(mdToPdfConfig);

      containerBuilder.RegisterType<MarkdownToPdfConvertor>()
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

      containerBuilder.RegisterInstance(executionContextAccessor);
      containerBuilder.RegisterInstance(httpClientFactory);

      _container = containerBuilder.Build();

      LearningCompositionRoot.SetContainer(_container);
    }
  }
}
