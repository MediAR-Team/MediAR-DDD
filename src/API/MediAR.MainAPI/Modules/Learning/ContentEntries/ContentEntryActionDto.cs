namespace MediAR.MainAPI.Modules.Learning.ContentEntries
{
  public class ContentEntryActionDto
  {
    public string TypeName { get; set; }
    public string ActionName { get; set; }
    public dynamic Payload { get; set; }
  }
}
