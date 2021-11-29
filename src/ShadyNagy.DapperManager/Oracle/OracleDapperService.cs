using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using ShadyNagy.Dapper.SharedKernel.Interfaces;
using ShadyNagy.DapperManager.Interfaces;

namespace ShadyNagy.DapperManager.Oracle
{
  public class OracleDapperService : IDapperService
  {
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly ISyntaxBuilder _oracleSyntaxBuilder;
    private ILogger<OracleDapperService> _logger { get; }

    public OracleDapperService(ISqlConnectionFactory sqlConnectionFactory, ISyntaxBuilder oracleSyntaxBuilder, ILogger<OracleDapperService> logger)
    {
      _sqlConnectionFactory = sqlConnectionFactory;
      _oracleSyntaxBuilder = oracleSyntaxBuilder;
      _logger = logger;
      _oracleSyntaxBuilder.Reset();
    }

    public async Task<List<T>> GetFromAsync<T>(string name)
    {
      try
      {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        return (await connection.QueryAsync<T>(_oracleSyntaxBuilder.SelectAllFrom(name).Build(), commandType: CommandType.Text)).ToList();
      }
      catch (Exception exception)
      {
        _logger.LogError(exception.Message);
        return null;
      }
    }

    public async Task<List<T>> GetColumnsFromAsync<T>(string name, List<string> columnsNames)
    {
      try
      {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        var oracleSyntaxBuilder = new OracleSyntaxBuilder();

        return (await connection.QueryAsync<T>(oracleSyntaxBuilder.SelectColumnsFrom(name, columnsNames).Build(), commandType: CommandType.Text)).ToList();
      }
      catch (Exception exception)
      {
        _logger.LogError(exception.Message);
        return null;
      }
    }

    public List<T> GetFrom<T>(string name)
    {
      try
      {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        return connection.Query<T>(_oracleSyntaxBuilder.SelectAllFrom(name).Build(), commandType: CommandType.Text).ToList();
      }
      catch (Exception exception)
      {
        _logger.LogError(exception.Message);
        return null;
      }
    }

    public List<T> GetColumnsFrom<T>(string name, List<string> columnsNames)
    {
      try
      {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        var oracleSyntaxBuilder = new OracleSyntaxBuilder();

        return connection.Query<T>(oracleSyntaxBuilder.SelectColumnsFrom(name, columnsNames).Build(), commandType: CommandType.Text).ToList();
      }
      catch (Exception exception)
      {
        _logger.LogError(exception.Message);
        return null;
      }
    }

    public async Task<int> InsertAsync<T>(string tableFullName, object toInsert)
    {
      try
      {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        return await connection.ExecuteAsync(_oracleSyntaxBuilder.Insert(tableFullName, toInsert).Build(), commandType: CommandType.Text);
      }
      catch (Exception exception)
      {
        _logger.LogError(exception.Message);
        return -1;
      }
    }

    public async Task<int> InsertSafeAsync<T>(string tableFullName, object toInsert, Dictionary<string, string> mapper)
    {
      try
      {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        return await connection.ExecuteAsync(_oracleSyntaxBuilder.InsertSafe(tableFullName, toInsert, mapper).Build(), toInsert);
      }
      catch (Exception exception)
      {
        _logger.LogError(exception.Message);
        return -1;
      }
    }

    public int Insert<T>(string tableFullName, object toInsert)
    {
      try
      {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        return connection.Execute(_oracleSyntaxBuilder.Insert(tableFullName, toInsert).Build(), commandType: CommandType.Text);
      }
      catch (Exception exception)
      {
        _logger.LogError(exception.Message);
        return -1;
      }
    }

    public int InsertSafe<T>(string tableFullName, object toInsert, Dictionary<string, string> mapper)
    {
      try
      {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        return connection.Execute(_oracleSyntaxBuilder.InsertSafe(tableFullName, toInsert, mapper).Build(), toInsert);
      }
      catch (Exception exception)
      {
        _logger.LogError(exception.Message);
        return -1;
      }
    }
  }
}
