﻿using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using ShadyNagy.Dapper.SharedKernel.Interfaces;

namespace ShadyNagy.DapperManager.Microsoft
{
  public class MicrosoftConnectionFactory : ISqlConnectionFactory, IDisposable
  {
    private readonly string _connectionString;
    private IDbConnection _connection;

    public MicrosoftConnectionFactory(string connectionString)
    {
      _connectionString = connectionString;
    }

    public IDbConnection GetOpenConnection()
    {
      if (_connection == null || _connection.State != ConnectionState.Open)
      {
        _connection = new OracleConnection(_connectionString);
        _connection.Open();
      }

      return _connection;
    }

    public void Dispose()
    {
      if (_connection != null && _connection.State == ConnectionState.Open)
      {
        _connection.Dispose();
      }
    }
  }
}
