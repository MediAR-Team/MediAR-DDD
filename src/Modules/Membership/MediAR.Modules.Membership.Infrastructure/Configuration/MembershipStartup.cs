using Autofac;
using MediAR.Coreplatform.Application;
using MediAR.Modules.Membership.Infrastructure.Configuration.Authentication;
using MediAR.Modules.Membership.Infrastructure.Configuration.DataAccess;
using MediAR.Modules.Membership.Infrastructure.Configuration.EventBus;
using MediAR.Modules.Membership.Infrastructure.Configuration.Mediation;
using MediAR.Modules.Membership.Infrastructure.Configuration.Processing;
using MediAR.Modules.Membership.Infrastructure.Configuration.Quartz;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace MediAR.Modules.Membership.Infrastructure.Configuration
{
  public static class MembershipStartup
  {
    private static IContainer _container;

    public static void Initialize(IConfiguration configuration, IExecutionContextAccessor executionContextAccessor, ILogger logger)
    {
      ConfigureCompositionRoot(configuration, executionContextAccessor, logger);

      QuartzStartup.Initialize();
    }

    private static void ConfigureCompositionRoot(IConfiguration configuration, IExecutionContextAccessor executionContextAccessor, ILogger logger)
    {
      var containerBuilder = new ContainerBuilder();

      containerBuilder
        .RegisterInstance(logger.ForContext("Module", "Membership"))
        .As<ILogger>()
        .SingleInstance();

      containerBuilder.RegisterModule(new DataAccessModule(configuration));
      containerBuilder.RegisterModule(new AuthenticationModule(configuration));
      containerBuilder.RegisterModule(new EventBusModule(null));
      containerBuilder.RegisterModule(new ProcessingModule());
      containerBuilder.RegisterModule(new MediatorModule());

      containerBuilder.RegisterInstance(executionContextAccessor);

      _container = containerBuilder.Build();

      MembershipCompositionRoot.SetContainer(_container);
    }
  }
}
