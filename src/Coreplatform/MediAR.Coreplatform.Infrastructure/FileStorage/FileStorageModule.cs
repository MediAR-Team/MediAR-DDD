using Autofac;
using MediAR.Coreplatform.Application.FielStorage;
using Microsoft.Extensions.Configuration;

namespace MediAR.Coreplatform.Infrastructure.FileStorage
{
  public class FileStorageModule : Module
  {
    private readonly FileStorageConfig _fileStorageConfig;

    public FileStorageModule(IConfiguration configuration)
    {
      _fileStorageConfig = new FileStorageConfig();
      configuration.GetSection("minioConfig").Bind(_fileStorageConfig);
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterInstance(_fileStorageConfig)
        .AsSelf()
        .SingleInstance();

      builder.RegisterType<MinioFileStorage>()
        .As<IFileStorage>()
        .InstancePerLifetimeScope();
    }
  }
}
