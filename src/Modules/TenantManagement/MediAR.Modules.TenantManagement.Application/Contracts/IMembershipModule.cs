using System.Threading.Tasks;

namespace MediAR.Modules.TenantManagement.Application.Contracts
{
  public interface IMembershipModule
  {
    Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);
    Task ExecuteCommandAsync(ICommand command);
    Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> command);
  }
}
