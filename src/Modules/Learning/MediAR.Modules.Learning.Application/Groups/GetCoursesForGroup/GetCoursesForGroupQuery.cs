using MediAR.Modules.Learning.Application.Contracts;
using System.Collections.Generic;

namespace MediAR.Modules.Learning.Application.Groups.GetCoursesForGroup
{
  public class GetCoursesForGroupQuery : QueryBase<List<CourseDto>>
  {
    public int GroupId { get; }

    public GetCoursesForGroupQuery(int groupId)
    {
      GroupId = groupId;
    }
  }
}
