using MediAR.Modules.Membership.Application.Contracts;
using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Application.Configuration.Commands
{
  public interface ICommandsScheduler
  {
    Task ScheduleAsync(ICommand command);
  }
}
