using System;

namespace MediAR.Modules.Learning.Application.Courses.CreateCourse
{
  public class CourseDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string BackgroundImageUrl { get; set; }
    public Guid TenantId { get; set; }
  }
}
