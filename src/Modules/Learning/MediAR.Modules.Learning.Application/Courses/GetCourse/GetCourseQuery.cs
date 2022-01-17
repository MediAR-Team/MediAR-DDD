using MediAR.Modules.Learning.Application.Contracts;
using System;

namespace MediAR.Modules.Learning.Application.Courses.GetCourse
{
  public class GetCourseQuery : QueryBase<CourseAggregateDto>
  {
    public int CourseId { get; }
    public Guid? TenantId { get; }

    public GetCourseQuery(int courseId, Guid? tenantId)
    {
      CourseId = courseId;
      TenantId = tenantId;
    }

    public GetCourseQuery(int courseId) : this(courseId, null) { }
  }
}
