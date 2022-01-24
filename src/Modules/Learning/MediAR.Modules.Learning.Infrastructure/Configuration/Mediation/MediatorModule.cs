using Autofac;
using MediAR.Coreplatform.Infrastructure.Mediation;
using MediAR.Modules.Learning.Application.Groups.CreateGroup;

namespace MediAR.Modules.Learning.Infrastructure.Configuration.Mediation
{
  class MediatorModule : Autofac.Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterMediator(ThisAssembly, typeof(CreateGroupCommand).Assembly);
    }
  }
}
