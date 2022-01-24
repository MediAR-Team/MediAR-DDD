using Autofac;
using MediAR.Coreplatform.Infrastructure.Mediation;
using MediAR.Modules.Membership.Application.Users.RegisterUser;

namespace MediAR.Modules.Membership.Infrastructure.Configuration.Mediation
{
  class MediatorModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterMediator(ThisAssembly, typeof(RegisterUserCommand).Assembly);
    }
  }
}
