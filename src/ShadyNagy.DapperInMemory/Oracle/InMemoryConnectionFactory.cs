using ShadyNagy.Dapper.SharedKernel.Interfaces;
using System;
using System.Data;

namespace ShadyNagy.DapperInMemory.Oracle
{
    public class InMemoryConnectionFactory : ISqlConnectionFactory, IDisposable
    {
        private readonly string _connectionString;
        private IDbConnection _connection;

        public InMemoryConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetOpenConnection()
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                _connection = new InMemoryConnection(_connectionString);
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
