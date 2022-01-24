using MediAR.Modules.Learning.Application.Contracts;
using System;

namespace MediAR.Modules.Learning.Application.Courses.CreateCourse
{
  public class CreateCourseCommand : CommandBase
  {
    public string Name { get; }
    public string Description { get; }
    public string BackgroundImageUrl { get; }
    public Guid? TenantId { get; }

    public CreateCourseCommand(string name, string description, string backgroundImageUrl, Guid? tenantId = null)
    {
      Name = name;
      Description = description;
      BackgroundImageUrl = backgroundImageUrl;
      TenantId = tenantId;
    }
  }
}
