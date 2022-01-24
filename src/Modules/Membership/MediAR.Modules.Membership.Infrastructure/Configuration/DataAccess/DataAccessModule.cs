using Autofac;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Infrastructure.Data;
using Microsoft.Extensions.Configuration;

namespace MediAR.Modules.Membership.Infrastructure.Configuration.DataAccess
{
  class DataAccessModule : Module
  {
    private readonly IConfiguration _configuration;

    public DataAccessModule(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<SqlConnectionFactory>()
        .As<ISqlConnectionFactory>()
        .InstancePerLifetimeScope();

      builder.RegisterType<SqlFacade>()
        .As<ISqlFacade>()
        .InstancePerLifetimeScope();

      builder.Register(x =>
      {
        var sqlConfig = new SqlConfiguration();
        _configuration.GetSection("sqlConfig").Bind(sqlConfig);

        return sqlConfig;
      })
        .AsSelf()
        .SingleInstance();
    }
  }
}
