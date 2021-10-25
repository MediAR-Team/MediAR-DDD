using MediAR.Modules.Learning.Application.Contracts;
using System;
using System.Collections.Generic;

namespace MediAR.Modules.Learning.Application.Groups.GetGroupMembers
{
  public class GetGroupMembersQuery : QueryBase<List<GroupMemberDto>>
  {
    public int GroupId { get; }
    public Guid? TenantId { get; }

    public GetGroupMembersQuery(int groupId) : this(groupId, null) { }

    public GetGroupMembersQuery(int groupId, Guid? tenantId)
    {
      GroupId = groupId;
      TenantId = tenantId;
    }
  }
}
