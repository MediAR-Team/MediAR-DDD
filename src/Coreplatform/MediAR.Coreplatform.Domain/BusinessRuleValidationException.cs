using System;

namespace MediAR.Coreplatform.Domain
{
  public class BusinessRuleValidationException : Exception
  {
    public string Message { get; }

    public BusinessRuleValidationException(string message) : base(message)
    {
      Message = message;
    }
  }
}
