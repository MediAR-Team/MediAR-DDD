using System;

namespace MediAR.Coreplatform.Application.Exceptions
{
  public class NotFoundException : Exception
  {
    public NotFoundException(string message) : base(message)
    {

    }
  }
}
