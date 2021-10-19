using Autofac;
using MediAR.Modules.TenantManagement.Application.Tenants.CreateTenant;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace MediAR.Modules.TenantManagement.Infrastructure.Configuration.Mediation
{
  class MediatorModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterMediatR(typeof(MediatorModule).Assembly, typeof(CreateTenantCommand).Assembly);
    }
  }
}
