using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using ShadyNagy.Dapper.SharedKernel.Interfaces;
using ShadyNagy.DapperManager.Interfaces;

namespace ShadyNagy.DapperManager.Microsoft
{
  public class MicrosoftDapperService : IDapperService
  {
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly ISyntaxBuilder _microsoftSyntaxBuilder;
    private ILogger<MicrosoftDapperService> _logger { get; }

    public MicrosoftDapperService(ISqlConnectionFactory sqlConnectionFactory, ISyntaxBuilder microsoftSyntaxBuilder, ILogger<MicrosoftDapperService> logger)
    {
      _sqlConnectionFactory = sqlConnectionFactory;
      _microsoftSyntaxBuilder = microsoftSyntaxBuilder;
      _logger = logger;
      _microsoftSyntaxBuilder.Reset();
    }

    public async Task<List<T>> GetFromAsync<T>(string name)
    {
      try
      {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        return (await connection.QueryAsync<T>(_microsoftSyntaxBuilder.SelectAllFrom(name).Build(), commandType: CommandType.Text)).ToList();
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

        var microsoftSyntaxBuilder = new MicrosoftSyntaxBuilder();

        return (await connection.QueryAsync<T>(microsoftSyntaxBuilder.SelectColumnsFrom(name, columnsNames).Build(), commandType: CommandType.Text)).ToList();
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

        return connection.Query<T>(_microsoftSyntaxBuilder.SelectAllFrom(name).Build(), commandType: CommandType.Text).ToList();
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

        var microsoftSyntaxBuilder = new MicrosoftSyntaxBuilder();

        return connection.Query<T>(microsoftSyntaxBuilder.SelectColumnsFrom(name, columnsNames).Build(), commandType: CommandType.Text).ToList();
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

        return await connection.ExecuteAsync(_microsoftSyntaxBuilder.Insert(tableFullName, toInsert).Build(), commandType: CommandType.Text);
      }
      catch (Exception exception)
      {
        _logger.LogError(exception.Message);
        return -1;
      }
    }

    public async Task<int> InsertSafeAsync(string tableFullName, object toInsert, Dictionary<string , string> mapper)
    {
      try
      {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        return await connection.ExecuteAsync(_microsoftSyntaxBuilder.InsertSafe(tableFullName, toInsert, mapper).Build(), toInsert);
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

        return connection.Execute(_microsoftSyntaxBuilder.Insert(tableFullName, toInsert).Build(), commandType: CommandType.Text);
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

        return connection.Execute(_microsoftSyntaxBuilder.InsertSafe(tableFullName, toInsert, mapper).Build(), toInsert);
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

        return await connection.ExecuteAsync(_microsoftSyntaxBuilder.Update(tableFullName, keys, toUpdate).Build(), commandType: CommandType.Text);
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

        return await connection.ExecuteAsync(_microsoftSyntaxBuilder.UpdateSafe(tableFullName, toUpdate, keys, mapper).Build(), toUpdate);
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

        return connection.Execute(_microsoftSyntaxBuilder.Update(tableFullName, keys, toUpdate).Build(), commandType: CommandType.Text);
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

        return connection.Execute(_microsoftSyntaxBuilder.UpdateSafe(tableFullName, toInsert, keys, mapper).Build(), toInsert);
      }
      catch (Exception exception)
      {
        _logger.LogError(exception.Message);
        return -1;
      }
    }
  }
}
