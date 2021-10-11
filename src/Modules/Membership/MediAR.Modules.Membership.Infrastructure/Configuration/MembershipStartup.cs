using Autofac;
using MediAR.Coreplatform.Application;
using MediAR.Modules.Membership.Infrastructure.Configuration.DataAccess;
using MediAR.Modules.Membership.Infrastructure.Configuration.Tenants;
using MediAR.Modules.Membership.Infrastructure.Domain;
using Microsoft.Extensions.Configuration;
using System;

namespace MediAR.Modules.Membership.Infrastructure.Configuration
{
  public static class MembershipStartup
  {
    private static IContainer _container;

    public static void Initialize(IConfiguration configuration, IExecutionContextAccessor executionContextAccessor)
    {
      ConfigureCompositionRoot(configuration, executionContextAccessor);
    }

    private static void ConfigureCompositionRoot(IConfiguration configuration, IExecutionContextAccessor executionContextAccessor)
    {
      var containerBuilder = new ContainerBuilder();

      containerBuilder.RegisterModule(new DomainModule());
      containerBuilder.RegisterModule(new DataAccessModule(configuration));
      containerBuilder.RegisterModule(new TenantModule(configuration));
      throw new NotImplementedException();
    }
  }
}
