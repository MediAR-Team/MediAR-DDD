namespace MediAR.MainAPI.Configuration.ErrorHandling
{
  public class DetailedErrorResponse : BaseErrorResponse
  {
    public string ExceptionType { get; set; }
    public string StackTrace { get; set; }
  }
}
