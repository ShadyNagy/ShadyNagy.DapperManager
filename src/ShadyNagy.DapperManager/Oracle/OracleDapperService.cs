using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using ShadyNagy.Dapper.SharedKernel.Interfaces;
using ShadyNagy.DapperManager.Extensions;
using ShadyNagy.DapperManager.Interfaces;
using ShadyNagy.DapperManager.Models;

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

    public List<T> GetByIdsFrom<T>(string name, Dictionary<string, object> fields)
    {
      try
      {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        return (connection.Query<T>(_oracleSyntaxBuilder.SelectByFrom(name, fields).Build(), commandType: CommandType.Text)).ToList();
      }
      catch (Exception exception)
      {
        _logger.LogError(exception.Message);
        return null;
      }
    }

    public List<T> GetByIdsFromSafe<T>(string name, Dictionary<string, string> fields, object idsObject)
    {
      try
      {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        return (connection.Query<T>(_oracleSyntaxBuilder.SelectByFromSafe(name, fields).Build(), idsObject, commandType: CommandType.Text)).ToList();
      }
      catch (Exception exception)
      {
        _logger.LogError(exception.Message);
        return null;
      }
    }

    public async Task<List<T>> GetByIdsFromAsync<T>(string name, Dictionary<string, object> fields)
    {
      try
      {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        return (await connection.QueryAsync<T>(_oracleSyntaxBuilder.SelectByFrom(name, fields).Build(), commandType: CommandType.Text)).ToList();
      }
      catch (Exception exception)
      {
        _logger.LogError(exception.Message);
        return null;
      }
    }

    public async Task<List<T>> GetByIdsFromSafeAsync<T>(string name, Dictionary<string, string> fields, object idsObject)
    {
      try
      {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        return (await connection.QueryAsync<T>(_oracleSyntaxBuilder.SelectByFromSafe(name, fields).Build(), idsObject, commandType: CommandType.Text)).ToList();
      }
      catch (Exception exception)
      {
        _logger.LogError(exception.Message);
        return null;
      }
    }

    public async Task<List<T>> GetByIdsFromSafeAsync<T>(string name, object databaseFields)
    {
      try
      {
        var connection = _sqlConnectionFactory.GetOpenConnection();
        var parameters = databaseFields.ToDynamicParameters();

        return (await connection.QueryAsync<T>(_oracleSyntaxBuilder.SelectByFromSafe(name, databaseFields).Build(), parameters, commandType: CommandType.Text)).ToList();
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

    public async Task<int> InsertAsync(string tableFullName, object toInsert)
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

    public async Task<int> InsertSafeAsync(string tableFullName, object toInsert, Dictionary<string, string> mapper)
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

    public int Insert(string tableFullName, object toInsert)
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

    public int InsertSafe(string tableFullName, object toInsert, Dictionary<string, string> mapper)
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

    public async Task<int> UpdateAsync(string tableFullName, Dictionary<string, object> keys, object toUpdate)
    {
      try
      {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        return await connection.ExecuteAsync(_oracleSyntaxBuilder.Update(tableFullName, keys, toUpdate).Build(), commandType: CommandType.Text);
      }
      catch (Exception exception)
      {
        _logger.LogError(exception.Message);
        return -1;
      }
    }

    public async Task<int> UpdateSafeAsync(string tableFullName, object toUpdate, Dictionary<string, string> keys, Dictionary<string, string> mapper)
    {
      try
      {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        return await connection.ExecuteAsync(_oracleSyntaxBuilder.UpdateSafe(tableFullName, toUpdate, keys, mapper).Build(), toUpdate);
      }
      catch (Exception exception)
      {
        _logger.LogError(exception.Message);
        return -1;
      }
    }

    public int Update(string tableFullName, Dictionary<string, object> keys, object toUpdate)
    {
      try
      {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        return connection.Execute(_oracleSyntaxBuilder.Update(tableFullName, keys, toUpdate).Build(), commandType: CommandType.Text);
      }
      catch (Exception exception)
      {
        _logger.LogError(exception.Message);
        return -1;
      }
    }

    public int UpdateSafe(string tableFullName, object toInsert, Dictionary<string, string> keys, Dictionary<string, string> mapper)
    {
      try
      {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        return connection.Execute(_oracleSyntaxBuilder.UpdateSafe(tableFullName, toInsert, keys, mapper).Build(), toInsert);
      }
      catch (Exception exception)
      {
        _logger.LogError(exception.Message);
        return -1;
      }
    }
  }
}
