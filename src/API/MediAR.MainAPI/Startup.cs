using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediAR.Coreplatform.Application;
using MediAR.MainAPI.Configuration.Authorization;
using MediAR.MainAPI.Configuration.ErrorHandling;
using MediAR.MainAPI.Configuration.ExecutionContext;
using MediAR.Modules.Membership.Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MediAR.MainAPI
{
  public class Startup
  {
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();

      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();

      services.AddAuthorization(options =>
      {
        options.AddPolicy(HasPermissionAttribute.HasPermissionPolicyName, policyBuilder =>
        {
          policyBuilder.Requirements.Add(new HasPermissionAuthorizationRequirement());
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
      var executionContextAccessor = new ExecutionContextAccessor(httpContextAccessor);

      MembershipStartup.Initialize(_configuration, executionContextAccessor);
    }
  }
}