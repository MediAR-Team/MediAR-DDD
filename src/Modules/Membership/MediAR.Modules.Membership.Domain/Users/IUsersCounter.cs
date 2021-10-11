using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Domain.Users
{
  public interface IUsersCounter
  {
    Task<int> CountUsersWithUserNameOrEmailAsync(string userName, string email);
  }
}
