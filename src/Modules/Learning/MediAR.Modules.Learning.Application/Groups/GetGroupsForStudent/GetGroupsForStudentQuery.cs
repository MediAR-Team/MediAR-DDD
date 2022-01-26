using MediAR.Modules.Learning.Application.Contracts;
using System;
using System.Collections.Generic;

namespace MediAR.Modules.Learning.Application.Groups.GetGroupsForStudent
{
  [Obsolete("We don't need this endpoint")]
  public class GetGroupsForStudentQuery : QueryBase<List<GroupDto>>
  {
    public string UserIdentifier { get; }

    public GetGroupsForStudentQuery(string userIdentifier)
    {
      UserIdentifier = userIdentifier;
    }
  }
}
