using Autofac;
using MediAR.Modules.Membership.Domain.Users;

namespace MediAR.Modules.Membership.Infrastructure.Domain
{
  public class DomainModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<UsersCounter>()
        .As<IUsersCounter>()
        .InstancePerLifetimeScope();
    }
  }
}
