using Autofac;

namespace MediAR.Modules.Learning.Application.StudentSubmissions.Mapping
{
  public class SubmissionDataMapperFactory
  {
    private readonly ILifetimeScope _lifetimeScope;

    public SubmissionDataMapperFactory(ILifetimeScope lifetimeScope)
    {
      _lifetimeScope = lifetimeScope;
    }

    public ISubmissionDataMapper GetMapper(int typeId)
    {
      switch (typeId)
      {
        case 2:
          return _lifetimeScope.Resolve<SubmissionTaskSubmissionDataMapper>();
        default:
          return null;
      }
    }
  }
}
