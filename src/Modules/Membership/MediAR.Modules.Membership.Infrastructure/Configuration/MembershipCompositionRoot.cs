﻿using Autofac;

namespace MediAR.Modules.Membership.Infrastructure.Configuration
{
  internal static class MembershipCompositionRoot
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
