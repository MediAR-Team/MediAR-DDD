using Autofac;
using MediAR.Modules.Membership.Application.Configuration.Commands;
using MediAR.Modules.Membership.Application.Contracts;
using MediAR.Modules.Membership.Infrastructure.Configuration.Processing.InternalCommands;

namespace MediAR.Modules.Membership.Infrastructure.Configuration.Processing
{
  class ProcessingModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterGenericDecorator(typeof(LoggingCommandHandlerDecorator<>), typeof(ICommandHandler<>));
      builder.RegisterGenericDecorator(typeof(LoggingCommandHandlerWithResultDecorator<,>), typeof(ICommandHandler<,>));

      builder.RegisterGenericDecorator(typeof(ValidationCommandHandlerDecorator<>), typeof(ICommandHandler<>));
      builder.RegisterGenericDecorator(typeof(ValidationCommandHandlerDecorator<,>), typeof(ICommandHandler<,>));

      builder.RegisterType<InternalCommandScheduler>()
        .As<IInternalCommandScheduler>()
        .InstancePerLifetimeScope();
    }
  }
}
