using System;
using System.Collections.Generic;
using System.Data;

namespace ShadyNagy.DapperInMemory
{
    internal class InMemoryCommand : IDbCommand
    {
        private readonly Dictionary<string, DataSet> dataSets;
        private DataSet currentDataSet = new DataSet();

        public InMemoryCommand(Dictionary<string, DataSet> dataSets)
        {
            this.dataSets = dataSets;
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

        public string CommandText
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                if (dataSets.TryGetValue(value, out currentDataSet) == false)
                {
                    throw new Exception($"No DataSet configured for query '{value}', update your test setup");
                }
            }
        }

        public int CommandTimeout
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public CommandType CommandType
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

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

        public IDataReader ExecuteReader()
        {
            return currentDataSet.CreateDataReader();
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            return currentDataSet.CreateDataReader();
        }

        public object ExecuteScalar()
        {
            throw new NotImplementedException();
        }

        public void Prepare()
        {
            throw new NotImplementedException();
        }
    }
}
