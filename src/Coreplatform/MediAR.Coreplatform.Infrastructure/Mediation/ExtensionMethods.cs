using Autofac;
using MediatR;
using MediatR.Pipeline;
using System.Reflection;

namespace MediAR.Coreplatform.Infrastructure.Mediation
{
  public static class ExtensionMethods
  {
    public static void RegisterMediator(this ContainerBuilder builder, params Assembly[] assemblies)
    {
      builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

      var mediatorOpenTypes = new[]
      {
          typeof(IRequestHandler<>),
          typeof(IRequestHandler<,>),
          typeof(INotificationHandler<>),
      };

      builder.RegisterSource(new ScopedContravariantRegistrationSource(mediatorOpenTypes));

      foreach (var mediatorOpenType in mediatorOpenTypes)
      {
        builder
            .RegisterAssemblyTypes(assemblies)
            .AsClosedTypesOf(mediatorOpenType)
            .AsImplementedInterfaces()
            .FindConstructorsWith(new AllConstructorFinder());
      }

      builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
      builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

      builder.Register<ServiceFactory>(ctx =>
      {
        var c = ctx.Resolve<IComponentContext>();
        return t => c.Resolve(t);
      }).InstancePerLifetimeScope();
    }
  }
}
