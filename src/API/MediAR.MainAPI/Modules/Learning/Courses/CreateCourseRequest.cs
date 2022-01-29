namespace MediAR.MainAPI.Modules.Learning.Courses
{
  public class CreateCourseRequest
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public string FileType { get; set; }
    public string BackgroundImage { get; set; }
  }
}
