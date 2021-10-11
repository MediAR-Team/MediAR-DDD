using MediAR.Coreplatform.Application.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MediAR.Coreplatform.Infrastructure.Data
{
  public class SqlConnectionFactory : ISqlConnectionFactory, IDisposable
  {
    private readonly SqlConfiguration _config;
    private SqlConnection _connection;

    public SqlConnectionFactory(SqlConfiguration config)
    {
      _config = config;
    }

    public IDbConnection CreateNewConnection()
    {
      var connection = new SqlConnection(_config.ConnectionString);
      connection.Open();

      return connection;
    }

    public void Dispose()
    {
      if (_connection is not null || _connection.State == ConnectionState.Closed)
      {
        _connection.Dispose();
      }
    }

    public string GetConnectionString()
    {
      return _config.ConnectionString;
    }

    public IDbConnection GetOpenConnection()
    {
      if (_connection is null || _connection.State != ConnectionState.Open)
      {
        _connection = new SqlConnection(_config.ConnectionString);
        _connection.Open();
      }

      return _connection;
    }
  }
}
