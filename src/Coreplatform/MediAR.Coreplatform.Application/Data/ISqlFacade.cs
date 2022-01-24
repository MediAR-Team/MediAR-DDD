using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MediAR.Coreplatform.Application.Data
{
  public interface ISqlFacade
  {
    Task<IEnumerable<T>> QueryAsync<T>(string query, object param = null, CommandType commandType = CommandType.Text);
    Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, string splitOn = "Id", CommandType commandType = CommandType.Text);
    Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, string splitOn = "Id", CommandType commandType = CommandType.Text);
    Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, string splitOn = "Id", CommandType commandType = CommandType.Text);
    Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, string splitOn = "Id", CommandType commandType = CommandType.Text);
    Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, string splitOn = "Id", CommandType commandType = CommandType.Text);
    Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, string splitOn = "Id", CommandType commandType = CommandType.Text);
    Task<T> QueryFirstOrDefaultAsync<T>(string query, object param = null, CommandType commandType = CommandType.Text);
    Task ExecuteAsync(string query, object param = null, CommandType commandType = CommandType.Text);
    Task CommitAsync();
  }
}
