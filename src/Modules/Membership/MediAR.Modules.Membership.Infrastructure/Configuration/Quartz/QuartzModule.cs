using Autofac;
using Quartz;
using System.Linq;

namespace MediAR.Modules.Membership.Infrastructure.Configuration.Quartz
{
  class QuartzModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterAssemblyTypes(GetType().Assembly)
        .Where(x => typeof(IJob).IsAssignableFrom(x)).InstancePerDependency();
    }
  }
}
