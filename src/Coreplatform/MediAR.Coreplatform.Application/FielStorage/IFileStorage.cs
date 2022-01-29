using System;
using System.Threading.Tasks;

namespace MediAR.Coreplatform.Application.FielStorage
{
  public interface IFileStorage
  {
    Task UploadAsync(string base64File, string bucket, string fileName);
    Task UploadAsync(byte[] file, string bucket, string fileName);
    Task<string> GetUrlAsync(string bucket, string fileName, TimeSpan validTime);
  }
}
