using System;

namespace MediAR.Modules.Membership.Infrastructure.Configuration.Processing.InternalCommands
{
  class InternalCommandDto
  {
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public string Type { get; set; }
    public string Data { get; set; }
  }
}
