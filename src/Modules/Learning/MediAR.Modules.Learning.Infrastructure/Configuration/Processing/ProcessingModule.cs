using Autofac;
using MediAR.Modules.Learning.Application.Configuration.Commands;
using MediAR.Modules.Learning.Application.Contracts;
using MediAR.Modules.Learning.Infrastructure.Configuration.Processing.InternalCommands;

namespace MediAR.Modules.Learning.Infrastructure.Configuration.Processing
{
  class ProcessingModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterGenericDecorator(typeof(LoggingCommandHandlerDecorator<>), typeof(ICommandHandler<>));
      builder.RegisterGenericDecorator(typeof(LoggingCommandHandlerWithResultDecorator<,>), typeof(ICommandHandler<,>));

      builder.RegisterType<InternalCommandScheduler>()
        .As<IInternalCommandScheduler>()
        .InstancePerLifetimeScope();
    }
  }
}
