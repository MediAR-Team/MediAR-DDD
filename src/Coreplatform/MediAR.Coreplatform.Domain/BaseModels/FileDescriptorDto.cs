namespace MediAR.Coreplatform.Domain.BaseModels
{
  public class FileDescriptorDto
  {
    public string Url { get; }
    public string DisplayName { get; }

    public FileDescriptorDto(string url, string displayName)
    {
      Url = url;
      DisplayName = displayName;
    }
  }
}
