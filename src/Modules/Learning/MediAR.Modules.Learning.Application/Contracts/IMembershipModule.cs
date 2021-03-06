using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Contracts
{
  public interface ILearningModule
  {
    Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);
    Task ExecuteCommandAsync(ICommand command);
    Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> command);
  }
}
