using Autofac;
using MediAR.Modules.TenantManagement.Application.Configuration.Commands;

namespace MediAR.Modules.TenantManagement.Infrastructure.Configuration.Processing
{
  class ProcessingModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterGenericDecorator(typeof(LoggingCommandHandlerDecorator<>), typeof(ICommandHandler<>));
      builder.RegisterGenericDecorator(typeof(LoggingCommandHandlerWithResultDecorator<,>), typeof(ICommandHandler<,>));

      builder.RegisterGenericDecorator(typeof(ValidationCommandHandlerDecorator<>), typeof(ICommandHandler<>));
      builder.RegisterGenericDecorator(typeof(ValidationCommandHandlerDecorator<,>), typeof(ICommandHandler<,>));

      builder.RegisterGenericDecorator(typeof(UnitOfWorkCommandHandlerDecorator<>), typeof(ICommandHandler<>));
      builder.RegisterGenericDecorator(typeof(UnitOfWorkCommandHandlerDecorator<,>), typeof(ICommandHandler<,>));
    }
  }
}
