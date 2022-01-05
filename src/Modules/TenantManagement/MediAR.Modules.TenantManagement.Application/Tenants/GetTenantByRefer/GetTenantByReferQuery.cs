using MediAR.Modules.TenantManagement.Application.Contracts;

namespace MediAR.Modules.TenantManagement.Application.Tenants.GetTenantByRefer
{
  public class GetTenantByReferQuery : QueryBase<TenantDto>
  {
    public string ReferUrl { get; }

    public GetTenantByReferQuery(string referUrl)
    {
      ReferUrl = referUrl;
    }
  }
}
