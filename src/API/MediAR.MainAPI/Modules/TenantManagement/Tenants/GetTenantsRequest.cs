namespace MediAR.MainAPI.Modules.TenantManagement.Tenants
{
  public class GetTenantsRequest
  {
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
  }
}
