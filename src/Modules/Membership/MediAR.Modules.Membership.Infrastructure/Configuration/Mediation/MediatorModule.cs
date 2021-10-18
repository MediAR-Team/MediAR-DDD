using Autofac;
using MediAR.Modules.Membership.Application.Users.RegisterUser;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace MediAR.Modules.Membership.Infrastructure.Configuration.Mediation
{
  class MediatorModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterMediatR(typeof(MediatorModule).Assembly, typeof(RegisterUserCommand).Assembly);
    }
  }
}
