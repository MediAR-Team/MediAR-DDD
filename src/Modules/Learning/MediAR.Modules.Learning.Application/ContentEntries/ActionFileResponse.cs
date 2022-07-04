using System.IO;

namespace MediAR.Modules.Learning.Application.ContentEntries
{
  public class ActionFileResponse {
    public string FileType { get; }
    public Stream FileStream { get; }
    public byte[] FileData { get; }

    public ActionFileResponse(string fileType, Stream fileStream)
    {
      FileType = fileType;
      FileStream = fileStream;
    }
    
    public ActionFileResponse(string fileType, byte[] fileData)
    {
      FileType = fileType;
      FileData = fileData;
    }
  }
}