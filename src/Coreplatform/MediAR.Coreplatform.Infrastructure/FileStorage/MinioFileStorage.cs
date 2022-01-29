using MediAR.Coreplatform.Application.FielStorage;
using Minio;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MediAR.Coreplatform.Infrastructure.FileStorage
{
  internal class MinioFileStorage : IFileStorage
  {
    private readonly MinioClient _client;

    public MinioFileStorage(FileStorageConfig config)
    {
      _client = new MinioClient(config.ServerUrl,
                config.AccessKey,
                config.SecretKey
                );
  }

    public async Task<string> GetUrlAsync(string bucket, string fileName, TimeSpan validTime)
    {
      return await _client.PresignedGetObjectAsync(bucket, fileName, (int)validTime.TotalSeconds);
    }

    public async Task UploadAsync(string base64File, string bucket, string fileName)
    {
      var bytes = Convert.FromBase64String(base64File);
      await UploadAsync(bytes, bucket, fileName);

    }

    public async Task UploadAsync(byte[] file, string bucket, string fileName)
    {
      if (!(await _client.BucketExistsAsync(bucket))) {
        await _client.MakeBucketAsync(bucket);
      }

      using var stream = new MemoryStream(file);
      await _client.PutObjectAsync(bucket, fileName, stream, file.Length);
    }
  }
}
