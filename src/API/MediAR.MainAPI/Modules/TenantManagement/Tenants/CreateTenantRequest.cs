using System.ComponentModel.DataAnnotations;

namespace MediAR.MainAPI.Modules.TenantManagement.Tenants
{
  public class CreateTenantRequest
  {
    [Required]
    public string Name { get; set; }
    public string ConnectionString { get; set; }
  }
}
