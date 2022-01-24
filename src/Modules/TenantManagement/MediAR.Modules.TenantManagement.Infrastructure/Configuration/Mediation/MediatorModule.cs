using Autofac;
using MediAR.Coreplatform.Infrastructure.Mediation;
using MediAR.Modules.TenantManagement.Application.Tenants.CreateTenant;

namespace MediAR.Modules.TenantManagement.Infrastructure.Configuration.Mediation
{
  class MediatorModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterMediator(ThisAssembly, typeof(CreateTenantCommand).Assembly);
    }
  }
}
