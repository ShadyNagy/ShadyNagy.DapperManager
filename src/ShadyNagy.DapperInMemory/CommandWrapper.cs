using System;
using System.Data;
using System.Data.Common;

namespace ShadyNagy.DapperInMemory
{
    internal class CommandWrapper : DbCommand
    {
        private readonly InMemoryCommand inner;

        public CommandWrapper(InMemoryCommand inner)
        {
            this.inner = inner;
        }

        public override string CommandText
        {
            get => inner.CommandText;
            set => inner.CommandText = value;
        }

        public override int CommandTimeout
        {
            get => inner.CommandTimeout;
            set => inner.CommandTimeout = value;
        }

        public override CommandType CommandType
        {
            get => inner.CommandType;
            set => inner.CommandType = value;
        }

        public override bool DesignTimeVisible
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public override UpdateRowSource UpdatedRowSource
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        protected override DbConnection DbConnection
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        protected override DbParameterCollection DbParameterCollection => throw new NotImplementedException();

        protected override DbTransaction DbTransaction
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public override void Cancel()
        {
            throw new NotImplementedException();
        }

        public override int ExecuteNonQuery()
        {
            return inner.ExecuteNonQuery();
        }

        public override object ExecuteScalar()
        {
            throw new NotImplementedException();
        }

        public override void Prepare()
        {
            throw new NotImplementedException();
        }

        protected override DbParameter CreateDbParameter()
        {
            throw new NotImplementedException();
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            return new InMemoryDbDataReader(inner.ExecuteReader());
        }        
    }
}
