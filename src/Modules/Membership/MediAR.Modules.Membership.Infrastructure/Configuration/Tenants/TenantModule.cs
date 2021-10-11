using Autofac;
using MediAR.Coreplatform.Application.Tenants;
using Microsoft.Extensions.Configuration;

namespace MediAR.Modules.Membership.Infrastructure.Configuration.Tenants
{
  public class TenantModule : Module
  {
    private readonly IConfiguration _configuration;

    public TenantModule(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.Register(x =>
      {
        var tenantConfig = new TenantConfiguration();
        _configuration.GetSection("tenantConfig").Bind(tenantConfig);

        return tenantConfig;
      })
        .AsSelf()
        .SingleInstance();
    }
  }
}
