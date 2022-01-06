using MediAR.Coreplatform.Application.Queries;
using MediAR.Modules.Learning.Application.Contracts;
using MediAR.Modules.Learning.Application.Courses.GetCourse;
using System.Collections.Generic;

namespace MediAR.Modules.Learning.Application.Courses.GetCourses
{
  public class GetCoursesQuery : QueryBase<List<CourseDto>>, IPagedQuery
  {
    public int? Page { get; }
    public int? PageSize { get; }

    public GetCoursesQuery(int page, int pageSize)
    {
      Page = page;
      PageSize = pageSize;
    }
  }
}
