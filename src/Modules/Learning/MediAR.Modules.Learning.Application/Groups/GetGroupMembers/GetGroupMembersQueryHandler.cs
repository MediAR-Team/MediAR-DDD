using MediAR.Coreplatform.Application;
using MediAR.Coreplatform.Application.Data;
using MediAR.Coreplatform.Application.Tenants;
using MediAR.Modules.Learning.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Groups.GetGroupMembers
{
  class GetGroupMembersQueryHandler : IQueryHandler<GetGroupMembersQuery, List<GroupMemberDto>>
  {
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IExecutionContextAccessor _executionContextAccessor;
    private readonly TenantConfiguration _tenantConfig;

    public GetGroupMembersQueryHandler(ISqlConnectionFactory connectionFactory, IExecutionContextAccessor executionContextAccessor, TenantConfiguration tenantConfig)
    {
      _connectionFactory = connectionFactory;
      _executionContextAccessor = executionContextAccessor;
      _tenantConfig = tenantConfig;
    }

    public Task<List<GroupMemberDto>> Handle(GetGroupMembersQuery request, CancellationToken cancellationToken)
    {
      var connection = _connectionFactory.GetOpenConnection();

      var isMasterTenant = _executionContextAccessor.TenantId == _tenantConfig.MasterTenantId;

      //var sql = @"FROM [learning]"

      throw new NotImplementedException();
    }
  }
}
