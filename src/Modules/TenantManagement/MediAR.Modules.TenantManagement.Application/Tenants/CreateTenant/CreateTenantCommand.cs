using MediAR.Modules.TenantManagement.Application.Contracts;

namespace MediAR.Modules.TenantManagement.Application.Tenants.CreateTenant
{
  public class CreateTenantCommand : CommandBase<TenantDto>
  {
    public string Name { get; }
    public string ConnectionString { get; }

    public CreateTenantCommand(string name, string connectionString)
    {
      Name = name;
      ConnectionString = connectionString;
    }
  }
}
