using System;
using System.Collections.Generic;

namespace MediAR.Coreplatform.Application
{
  class InvalidCommandException : Exception
  {
    public List<string> Errors { get; }

    public InvalidCommandException(List<string> errors)
    {
      Errors = errors;
    }
  }
}
