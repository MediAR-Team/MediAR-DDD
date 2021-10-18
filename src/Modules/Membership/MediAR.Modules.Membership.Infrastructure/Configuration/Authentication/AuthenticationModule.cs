using Autofac;
using MediAR.Modules.Membership.Application.Authentication.TokenProviding;
using Microsoft.Extensions.Configuration;

namespace MediAR.Modules.Membership.Infrastructure.Configuration.Authentication
{
  class AuthenticationModule : Module
  {
    private readonly IConfiguration _configuration;

    public AuthenticationModule(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.Register(x =>
      {
        var tokenConfig = new TokenConfiguration();
        _configuration.GetSection("tokenConfig").Bind(tokenConfig);

        return tokenConfig;
      })
      .AsSelf()
      .SingleInstance();

      builder.RegisterType<TokenProvider>()
        .As<ITokenProvider>()
        .InstancePerLifetimeScope();
    }
  }
}
