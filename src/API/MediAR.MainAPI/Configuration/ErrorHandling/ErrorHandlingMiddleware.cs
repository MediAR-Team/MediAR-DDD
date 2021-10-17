using MediAR.Coreplatform.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace MediAR.MainAPI.Configuration.ErrorHandling
{
  public class ErrorHandlingMiddleware
  {
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      try
      {
        await _next.Invoke(context);
      }
      catch (Exception ex)
      {
        var statusCode = 500;
        var message = ex.Message;
        var stackTrace = ex.StackTrace;

        if (ex is BusinessRuleValidationException)
        {
          statusCode = 400;
        }

        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsync(JsonSerializer.Serialize(new DetailedErrorResponse
        {
          ExceptionMessage = message,
          ExceptionType = ex.GetType().Name,
          StackTrace = stackTrace
        }, typeof(DetailedErrorResponse)));
      }
    }
  }
}
