using MediAR.Modules.Learning.Application.Contracts;
using System;

namespace MediAR.Modules.Learning.Application.Courses.CreateCourse
{
  public class CreateCourseCommand : CommandBase
  {
    public string Name { get; }
    public string Description { get; }
    public string BackgroundImage { get; }
    public string FileType { get; set; }
    public Guid? TenantId { get; }

    public CreateCourseCommand(string name, string description, string fileType, string backgroundImage,Guid? tenantId = null)
    {
      Name = name;
      Description = description;
      FileType = fileType;
      BackgroundImage = backgroundImage;
      TenantId = tenantId;
    }
  }
}
