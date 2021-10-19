using Autofac;

namespace MediAR.Modules.TenantManagement.Infrastructure.Configuration
{
  internal static class TenantManagementCompositionRoot
  {
    private static IContainer _container;

    internal static void SetContainer(IContainer container)
    {
      _container = container;
    }

    internal static ILifetimeScope BeginLifetimeScope()
    {
      return _container.BeginLifetimeScope();
    }
  }
}
