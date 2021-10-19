using Autofac;
using MediAR.Modules.TenantManagement.Application.Contracts;
using MediAR.Modules.TenantManagement.Infrastructure;

namespace MediAR.MainAPI.Modules.TenantManagement
{
  public class TenantManagementAutofacModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<TenantManagementModule>()
        .As<ITenantManagementModule>()
        .InstancePerLifetimeScope();
    }
  }
}
