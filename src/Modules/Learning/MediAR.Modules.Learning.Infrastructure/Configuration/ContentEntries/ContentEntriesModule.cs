using Autofac;
using MediAR.Modules.Learning.Application.ContentEntries.TypeHandlers;
using MediAR.Modules.Learning.Infrastructure.Domain.ContentEntries;
using System.Linq;

namespace MediAR.Modules.Learning.Infrastructure.Configuration.ContentEntries
{
  class ContentEntriesModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<ContentEntriesRepository>()
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

      builder.RegisterType<ContentEntryHandlerFactory>()
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

      builder.RegisterAssemblyTypes(typeof(IContentEntryHandler).Assembly)
        .Where(x => x.IsClass && !x.IsAbstract && x.IsAssignableTo<IContentEntryHandler>())
        .As<IContentEntryHandler>()
        .InstancePerLifetimeScope();
    }
  }
}
