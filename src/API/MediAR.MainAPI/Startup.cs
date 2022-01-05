using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediAR.Coreplatform.Application;
using MediAR.MainAPI.Configuration.Authorization;
using MediAR.MainAPI.Configuration.ErrorHandling;
using MediAR.MainAPI.Configuration.ExecutionContext;
using MediAR.MainAPI.Modules.Learning;
using MediAR.MainAPI.Modules.Membership;
using MediAR.MainAPI.Modules.TenantManagement;
using MediAR.Modules.Learning.Infrastructure.Configuration;
using MediAR.Modules.Membership.Application.Authentication.TokenProviding;
using MediAR.Modules.Membership.Infrastructure.Configuration;
using MediAR.Modules.TenantManagement.Application.Contracts;
using MediAR.Modules.TenantManagement.Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace MediAR.MainAPI
{
  public class Startup
  {
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
      builder.RegisterModule(new MembershipAutofacModule());
      builder.RegisterModule(new TenantManagementAutofacModule());
      builder.RegisterModule(new LearningAutofacModule());
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();

      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();

      var tokenConfig = new TokenConfiguration();
      _configuration.GetSection("tokenConfig").Bind(tokenConfig);

      var secretBytes = Encoding.UTF8.GetBytes(tokenConfig.JwtSecret);
      var key = new SymmetricSecurityKey(secretBytes);

      services.AddAuthentication("Bearer")
        .AddJwtBearer("Bearer", options =>
        {
          options.RequireHttpsMetadata = false;
          options.TokenValidationParameters = new TokenValidationParameters
          {
            IssuerSigningKey = key,
            ValidateIssuer = true,
            ValidIssuer = tokenConfig.JwtIssuer,
            ValidateAudience = true,
            ValidAudience = tokenConfig.JwtAudience
          };
        });

      services.AddAuthorization(options =>
      {
        options.AddPolicy(HasPermissionAttribute.HasPermissionPolicyName, policyBuilder =>
        {
          policyBuilder.Requirements.Add(new HasPermissionAuthorizationRequirement());

          policyBuilder.AddAuthenticationSchemes("Bearer");
          // TODO: add jwtBearer authentication
        });
      });

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
    {
      var container = app.ApplicationServices.GetAutofacRoot();

      InitializeModules(container);

      app.UseMiddleware<ErrorHandlingMiddleware>();

      app.UseRouting();

      app.UseAuthentication();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }

    private void InitializeModules(ILifetimeScope container)
    {
      var httpContextAccessor = container.Resolve<IHttpContextAccessor>();
      var tenantManagement = container.Resolve<ITenantManagementModule>();
      var executionContextAccessor = new ExecutionContextAccessor(httpContextAccessor, tenantManagement);

      MembershipStartup.Initialize(_configuration, executionContextAccessor);
      TenantManagementStartup.Initialize(_configuration, executionContextAccessor);
      LearningStartup.Initialize(_configuration, executionContextAccessor);
    }
  }
}
