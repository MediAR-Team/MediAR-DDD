using Autofac;

namespace MediAR.Modules.Learning.Infrastructure.Configuration
{
  internal static class LearningCompositionRoot
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
