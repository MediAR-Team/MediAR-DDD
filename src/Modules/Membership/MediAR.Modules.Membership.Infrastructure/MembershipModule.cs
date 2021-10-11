using MediAR.Modules.Membership.Application.Contracts;
using System;
using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Infrastructure
{
  public class MembershipModule : IMembershipModule
  {
    public Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
    {
      throw new NotImplementedException();
    }

    public Task ExecuteCommandAsync(ICommand command)
    {
      throw new NotImplementedException();
    }

    public Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> command)
    {
      throw new NotImplementedException();
    }
  }
}
