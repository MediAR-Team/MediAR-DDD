using Autofac;
using MediatR;
using System.Reflection;

namespace MediAR.Modules.Membership.Infrastructure.Configuration.Mediation
{
  class MediatorModule : Autofac.Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

    }
  }
}
