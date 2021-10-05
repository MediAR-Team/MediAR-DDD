using System.Data;

namespace MediAR.Coreplatform.Application.Data
{
  public interface ISqlConnectionFactory
  {
    IDbConnection GetOpenConnection();
    IDbConnection CreateNewConnection();
    string GetConnectionString();
  }
}
