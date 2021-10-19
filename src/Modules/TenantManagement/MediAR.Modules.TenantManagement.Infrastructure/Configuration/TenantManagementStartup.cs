using Autofac;
using MediAR.Coreplatform.Application;
using MediAR.Modules.TenantManagement.Infrastructure.Configuration.DataAccess;
using MediAR.Modules.TenantManagement.Infrastructure.Configuration.Mediation;
using MediAR.Modules.TenantManagement.Infrastructure.Configuration.Processing;
using Microsoft.Extensions.Configuration;

namespace MediAR.Modules.TenantManagement.Infrastructure.Configuration
{
  public static class TenantManagementStartup
  {
    public static void Initialize(IConfiguration configuration, IExecutionContextAccessor executionContextAccessor)
    {
      ConfigureCompositionRoot(configuration, executionContextAccessor);
    }

    private static void ConfigureCompositionRoot(IConfiguration configuration, IExecutionContextAccessor executionContextAccessor)
    {
      var containerBuilder = new ContainerBuilder();

      containerBuilder.RegisterModule(new DataAccessModule(configuration));
      containerBuilder.RegisterModule(new MediatorModule());
      containerBuilder.RegisterModule(new ProcessingModule());

      containerBuilder.RegisterInstance(executionContextAccessor);

      var container = containerBuilder.Build();
      TenantManagementCompositionRoot.SetContainer(container);
    }
  }
}
