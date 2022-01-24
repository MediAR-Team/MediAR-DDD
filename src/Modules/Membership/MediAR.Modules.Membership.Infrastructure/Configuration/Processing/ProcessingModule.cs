using Autofac;
using MediAR.Modules.Membership.Application.Contracts;
using MediAR.Modules.Membership.Infrastructure.Configuration.Processing.InternalCommands;
using MediatR;

namespace MediAR.Modules.Membership.Infrastructure.Configuration.Processing
{
  class ProcessingModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterGenericDecorator(typeof(LoggingCommandHandlerDecorator<>), typeof(IRequestHandler<>));
      builder.RegisterGenericDecorator(typeof(LoggingCommandHandlerWithResultDecorator<,>), typeof(IRequestHandler<,>));

      builder.RegisterType<InternalCommandScheduler>()
        .As<IInternalCommandScheduler>()
        .InstancePerLifetimeScope();
    }
  }
}
