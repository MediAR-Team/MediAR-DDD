using Autofac;
using MediAR.Modules.Learning.Application.Contracts;
using MediAR.Modules.Learning.Infrastructure;

namespace MediAR.MainAPI.Modules.Learning
{
  public class LearningAutofacModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<LearningModule>()
        .As<ILearningModule>()
        .InstancePerLifetimeScope();
    }
  }
}
