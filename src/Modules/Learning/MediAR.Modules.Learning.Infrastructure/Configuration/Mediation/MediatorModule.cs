using Autofac;
using MediAR.Modules.Learning.Application.Groups.CreateGroup;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace MediAR.Modules.Learning.Infrastructure.Configuration.Mediation
{
  class MediatorModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterMediatR(typeof(MediatorModule).Assembly, typeof(CreateGroupCommand).Assembly);
    }
  }
}
