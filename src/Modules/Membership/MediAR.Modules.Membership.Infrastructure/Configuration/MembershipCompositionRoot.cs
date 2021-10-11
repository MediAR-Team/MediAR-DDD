using Autofac;

namespace MediAR.Modules.Membership.Infrastructure.Configuration
{
  static class MembershipCompositionRoot
  {
    private static IContainer _container;

    static void SetContainer(IContainer container)
    {
      _container = container;
    }

    static ILifetimeScope BeginLifetimeScope()
    {
      return _container.BeginLifetimeScope();
    }
  }
}
