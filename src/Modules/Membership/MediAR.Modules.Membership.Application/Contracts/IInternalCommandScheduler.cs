using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Application.Contracts
{
    public interface IInternalCommandScheduler
    {
        Task EnqueueAsync(ICommand command);
    }
}
