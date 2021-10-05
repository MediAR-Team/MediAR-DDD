using System.Threading.Tasks;

namespace MediAR.Coreplatform.Application.Outbox
{
  public interface IOutbox
  {
    void Add(OutboxMessage message);
    Task Save();
  }
}
