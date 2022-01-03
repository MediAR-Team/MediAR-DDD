using MediAR.Modules.Learning.Application.Contracts;
using System.Collections.Generic;

namespace MediAR.Modules.Learning.Application.Groups.GetGroupsForStudent
{
  public class GetGroupsForStudentQuery : QueryBase<List<GroupDto>>
  {
    public string UserIdentifier { get; }

    public GetGroupsForStudentQuery(string userIdentifier)
    {
      UserIdentifier = userIdentifier;
    }
  }
}
