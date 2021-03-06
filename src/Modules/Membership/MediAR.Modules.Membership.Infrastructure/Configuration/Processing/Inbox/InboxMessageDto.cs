using System;

namespace MediAR.Modules.Membership.Infrastructure.Configuration.Processing.Inbox
{
  class InboxMessageDto
  {
    public Guid Id { get; set; }
    public DateTime OccuredOn { get; set; }
    public string Type { get; set; }
    public string Data { get; set; }
  }
}
