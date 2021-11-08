using System.Threading.Tasks;

namespace MediAR.Modules.Learning.Application.Contracts
{
    public interface IInternalCommandScheduler
    {
        Task EnqueueAsync(ICommand command);
    }
}
