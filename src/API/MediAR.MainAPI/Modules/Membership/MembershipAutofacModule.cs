using Autofac;
using MediAR.Modules.Membership.Application.Contracts;
using MediAR.Modules.Membership.Infrastructure;

namespace MediAR.MainAPI.Modules.Membership
{
  public class MembershipAutofacModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<MembershipModule>()
        .As<IMembershipModule>()
        .InstancePerLifetimeScope();
    }
  }
}
