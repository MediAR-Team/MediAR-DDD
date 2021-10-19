using MediAR.Coreplatform.Application.Queries;
using MediAR.Modules.Learning.Application.Contracts;
using System.Collections.Generic;

namespace MediAR.Modules.Learning.Application.Groups.GetGroups
{
  public class GetGroupsQuery : QueryBase<List<GroupDto>>, IPagedQuery
  {
    public int? Page { get; }
    public int? PageSize { get; }

    public GetGroupsQuery(int page, int pageSize)
    {
      Page = page;
      PageSize = pageSize;
    }
  }
}
