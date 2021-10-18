using MediAR.Coreplatform.Application.Queries;
using MediAR.Modules.TenantManagement.Application.Contracts;
using System.Collections.Generic;

namespace MediAR.Modules.TenantManagement.Application.Tenants.GetTenants
{
  public class GetTenantsQuery : QueryBase<List<TenantDto>>, IPagedQuery
  {
    public int? Page { get; }
    public int? PageSize { get; }

    public GetTenantsQuery(int page, int pageSize)
    {
      Page = page;
      PageSize = pageSize;
    }
  }
}
