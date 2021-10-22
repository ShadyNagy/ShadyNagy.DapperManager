using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ShadyNagy.DapperInMemory
{
    internal class InMemoryCommand : IDbCommand
    {
        private readonly Dictionary<string, DataSet> _dataSets;
        private DataSet _currentDataSet = new DataSet();

        public InMemoryCommand(Dictionary<string, DataSet> dataSets)
        {
            _dataSets = dataSets;
            _currentDataSet = dataSets.LastOrDefault().Value;
        }

        public IDbConnection Connection
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public IDbTransaction Transaction
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public string CommandText { get; set; }

        public int CommandTimeout { get; set; }       

        public CommandType CommandType { get; set; }       

        public IDataParameterCollection Parameters => throw new NotImplementedException();

        public UpdateRowSource UpdatedRowSource
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public IDbDataParameter CreateParameter()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public int ExecuteNonQuery()
        {
            throw new NotImplementedException();
        }

        public IDataReader? ExecuteReader()
        {
            if (CommandType == CommandType.Text)
            {
                if (GetSqlCommandType() == SqlSyntexType.Nothing)
                {
                    return null;
                }else if (GetSqlCommandType() == SqlSyntexType.Select)
                {
                    return ExcuteSelect();
                }
            }
            return _currentDataSet.CreateDataReader();
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            return _currentDataSet.CreateDataReader();
        }

        public object ExecuteScalar()
        {
            throw new NotImplementedException();
        }

        public void Prepare()
        {
            throw new NotImplementedException();
        }

        private string[] GetCommandTextParts()
        {
            if (string.IsNullOrEmpty(CommandText))
            {
                return new string[0];
            }

            return CommandText.Split(" ");
        }

        private SqlSyntexType GetSqlCommandType()
        {
            if (string.IsNullOrEmpty(CommandText))
            {
                return SqlSyntexType.Nothing;
            }
            var parts = GetCommandTextParts();
            if (parts.Length <= 1)
            {
                return SqlSyntexType.Nothing;
            }

            if (parts[0].ToLower() == "select")
            {
                return SqlSyntexType.Select;
            }else if (parts[0].ToLower() == "insert")
            {
                return SqlSyntexType.Insert;
            }
            else if (parts[0].ToLower() == "update")
            {
                return SqlSyntexType.Update;
            }

            return SqlSyntexType.Nothing;
        }

        private string GetSelectTableName()
        {
            var parts = GetCommandTextParts();
            if (parts.Length <= 3)
            {
                return string.Empty;
            }

            return parts[3];
        }

        private IDataReader? ExcuteSelect()
        {
            var tableName = GetSelectTableName();

            if (!_currentDataSet.Tables.Contains(tableName))
            {
                return null;
            }
            return _currentDataSet.CreateDataReader();
        }
    }
}
