using MediAR.Modules.Learning.Application.Contracts;
using System;

namespace MediAR.Modules.Learning.Application.Courses.CreateCourse
{
  public class CreateCourseCommand : CommandBase<CourseDto>
  {
    public string Name { get; }
    public string Description { get; }
    public string BackgroundImageUrl { get; }
    public Guid? TenantId { get; }

    public CreateCourseCommand(string name, string description, string backgroundImageUrl, Guid? tenantId)
    {
      Name = name;
      Description = description;
      BackgroundImageUrl = backgroundImageUrl;
      TenantId = tenantId;
    }

    public CreateCourseCommand(string name, string description, string backgroundImageUrl) : this(name, description, backgroundImageUrl, null) { }
  }
}
