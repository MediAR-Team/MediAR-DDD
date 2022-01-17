using MediAR.Modules.Learning.Application.Contracts;
using MediAR.Modules.Learning.Application.Groups.GetCoursesForGroup;
using System.Collections.Generic;

namespace MediAR.Modules.Learning.Application.Courses.GetCoursesForUser
{
  public class GetCoursesForUserQuery : QueryBase<List<CourseDto>>
  {
    public string UserIdentifier { get; }
    public UserIdentifierOption Option { get; }

    public GetCoursesForUserQuery(string userIdentifier, UserIdentifierOption option)
    {
      UserIdentifier = userIdentifier;
      Option = option;
    }
  }

  public enum UserIdentifierOption
  {
    UserName, Email
  }
}
