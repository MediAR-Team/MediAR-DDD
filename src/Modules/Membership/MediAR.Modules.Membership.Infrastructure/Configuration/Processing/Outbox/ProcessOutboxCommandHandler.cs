using MediAR.Coreplatform.Application.Data;
using MediAR.Modules.Membership.Application.Configuration.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediAR.Modules.Membership.Infrastructure.Configuration.Processing.Outbox
{
  class ProcessOutboxCommandHandler : ICommandHandler<ProcessOutboxCommand>
  {
    private readonly ISqlConnectionFactory _connectionFactory;

    public ProcessOutboxCommandHandler(ISqlConnectionFactory connectionFactory)
    {
      _connectionFactory = connectionFactory;
    }

    public Task<Unit> Handle(ProcessOutboxCommand request, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
  }
}
