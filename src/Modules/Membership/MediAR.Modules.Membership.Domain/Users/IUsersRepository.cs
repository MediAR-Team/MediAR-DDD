using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Domain.Users
{
  interface IUsersRepository
  {
    Task AddAsync();
  }
}
