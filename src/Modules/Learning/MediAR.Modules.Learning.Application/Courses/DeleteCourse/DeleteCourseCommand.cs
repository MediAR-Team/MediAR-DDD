using MediAR.Modules.Learning.Application.Contracts;
using System;

namespace MediAR.Modules.Learning.Application.Courses.DeleteCourse
{
  public class DeleteCourseCommand : CommandBase
  {
    public int CourseId { get; }
    public Guid? TenantId { get; }

    public DeleteCourseCommand(int courseId, Guid? tenantId)
    {
      CourseId = courseId;
      TenantId = tenantId;
    }

    public DeleteCourseCommand(int courseId) : this(courseId, null) { }
  }
}
