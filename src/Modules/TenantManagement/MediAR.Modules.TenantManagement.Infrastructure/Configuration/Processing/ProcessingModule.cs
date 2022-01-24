using Autofac;
using MediAR.Modules.TenantManagement.Application.Configuration.Commands;
using MediatR;

namespace MediAR.Modules.TenantManagement.Infrastructure.Configuration.Processing
{
  class ProcessingModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterGenericDecorator(typeof(LoggingCommandHandlerDecorator<>), typeof(IRequestHandler<>));
      builder.RegisterGenericDecorator(typeof(LoggingCommandHandlerWithResultDecorator<,>), typeof(IRequestHandler<,>));
    }
  }
}
