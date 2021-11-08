using Autofac;
using MediAR.Coreplatform.Application;
using MediAR.Modules.Membership.Infrastructure.Configuration.Authentication;
using MediAR.Modules.Membership.Infrastructure.Configuration.DataAccess;
using MediAR.Modules.Membership.Infrastructure.Configuration.EventBus;
using MediAR.Modules.Membership.Infrastructure.Configuration.Mediation;
using MediAR.Modules.Membership.Infrastructure.Configuration.Processing;
using MediAR.Modules.Membership.Infrastructure.Configuration.Quartz;
using MediAR.Modules.Membership.Infrastructure.Configuration.Tenants;
using Microsoft.Extensions.Configuration;

namespace MediAR.Modules.Membership.Infrastructure.Configuration
{
  public static class MembershipStartup
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
      containerBuilder.RegisterModule(new TenantModule(configuration));
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
