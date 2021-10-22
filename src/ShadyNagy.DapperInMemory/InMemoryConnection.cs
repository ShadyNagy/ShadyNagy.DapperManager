using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ShadyNagy.DapperInMemory
{
    public class InMemoryConnection : IDbConnection
    {
        private readonly Dictionary<string, DataSet> dataSets = new Dictionary<string, DataSet>();
        private string _connectionString = string.Empty;
        private ConnectionState _state = ConnectionState.Closed;
        private string _databaseName = string.Empty;
        public int _connectionTimeout = 60;

        public InMemoryConnection(string connectionString)
        {
            _connectionString = connectionString;

            _databaseName = GetDatabaseName(_connectionString);
            AddNewDatabase(_databaseName);
        }

        public string ConnectionString
        {
            get => _connectionString;
            set => _connectionString = value;
        }

        public int ConnectionTimeout => _connectionTimeout;

        public string Database => _databaseName;

        public ConnectionState State => _state;

        public IDbTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            throw new NotImplementedException();
        }

        public void ChangeDatabase(string databaseName)
        {
            _databaseName = databaseName;
            AddNewDatabase(databaseName);
        }

        public void Close()
        {
            _state = ConnectionState.Closed;
        }

        public IDbCommand CreateCommand()
        {
            return new CommandWrapper(new InMemoryCommand(dataSets));
        }

        public void Dispose()
        {
            Close();
            dataSets.Remove(_databaseName);
        }

        public void Open()
        {
            _state = ConnectionState.Open;
        }

        private void AddNewDatabase(string databaseName)
        {
            dataSets.Add(databaseName, new DataSet());
        }

        private string GetDatabaseName(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                return string.Empty;
            }
            var mainParts = connectionString.Split(";");
            if (mainParts.Length <= 0)
            {
                return string.Empty;
            }

            var databasePart = mainParts.FirstOrDefault(x => x.ToLower().Contains("database"))?.Split("=");
            if (databasePart == null || databasePart.Length <= 1)
            {
                return string.Empty;
            }

            return databasePart[1];
        }
    }
}
