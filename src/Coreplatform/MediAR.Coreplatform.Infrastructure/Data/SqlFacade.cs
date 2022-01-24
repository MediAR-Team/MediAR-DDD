using Dapper;
using MediAR.Coreplatform.Application.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MediAR.Coreplatform.Infrastructure.Data
{
  public class SqlFacade : ISqlFacade
  {
    private readonly SqlConnection _connection;
    private readonly SqlTransaction _transaction;

    public SqlFacade(ISqlConnectionFactory connectionFactory)
    {
      _connection = (SqlConnection)connectionFactory.GetOpenConnection();
      _transaction = _connection.BeginTransaction();
    }

    public async Task CommitAsync()
    {
      await _transaction.CommitAsync();
    }

    public async Task ExecuteAsync(string query, object param = null, CommandType commandType = CommandType.Text)
    {
      await _connection.ExecuteAsync(query, param, _transaction, commandType: commandType);
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string query, object param = null, CommandType commandType = CommandType.Text)
    {
      return await _connection.QueryAsync<T>(query, param, _transaction, commandType: commandType);
    }

    public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, string splitOn = "Id", CommandType commandType = CommandType.Text)
    {
      return await _connection.QueryAsync(sql, map, param, _transaction, splitOn: splitOn, commandType: commandType);
    }

    public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, string splitOn = "Id", CommandType commandType = CommandType.Text)
    {
      return await _connection.QueryAsync(sql, map, param, _transaction, splitOn: splitOn, commandType: commandType);
    }

    public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, string splitOn = "Id", CommandType commandType = CommandType.Text)
    {
      return await _connection.QueryAsync(sql, map, param, _transaction, splitOn: splitOn, commandType: commandType);
    }

    public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, string splitOn = "Id", CommandType commandType = CommandType.Text)
    {
      return await _connection.QueryAsync(sql, map, param, _transaction, splitOn: splitOn, commandType: commandType);
    }

    public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, string splitOn = "Id", CommandType commandType = CommandType.Text)
    {
      return await _connection.QueryAsync(sql, map, param, _transaction, splitOn: splitOn, commandType: commandType);
    }

    public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, string splitOn = "Id", CommandType commandType = CommandType.Text)
    {
      return await _connection.QueryAsync(sql, map, param, _transaction, splitOn: splitOn, commandType: commandType);
    }

    public async Task<T> QueryFirstOrDefaultAsync<T>(string query, object param = null, CommandType commandType = CommandType.Text)
    {
      return await _connection.QueryFirstOrDefaultAsync<T>(query, param, _transaction, commandType: commandType);
    }
  }
}
