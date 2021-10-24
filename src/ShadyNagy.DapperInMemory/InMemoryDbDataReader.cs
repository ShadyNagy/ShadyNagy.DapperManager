using System;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace ShadyNagy.DapperInMemory
{
    internal class InMemoryDbDataReader : DbDataReader
    {
        private readonly IDataReader dataReader;

        public InMemoryDbDataReader(IDataReader dataReader)
        {
            this.dataReader = dataReader;
        }

        public override int Depth => dataReader.Depth;

        public override int FieldCount => dataReader == null ? 0 : dataReader.FieldCount;

        public override bool HasRows => dataReader.Read();

        public override bool IsClosed => dataReader.IsClosed;

        public override int RecordsAffected => dataReader.RecordsAffected;

        public override object this[int ordinal] => dataReader[ordinal];

        public override object this[string name] => dataReader[name];

        public override bool GetBoolean(int ordinal)
        {
            return dataReader.GetBoolean(ordinal);
        }

        public override byte GetByte(int ordinal)
        {
            return dataReader.GetByte(ordinal);
        }

        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            return dataReader.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
        }

        public override char GetChar(int ordinal)
        {
            return dataReader.GetChar(ordinal);
        }

        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            return dataReader.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
        }

        public override string GetDataTypeName(int ordinal)
        {
            return dataReader.GetDataTypeName(ordinal);
        }

        public override DateTime GetDateTime(int ordinal)
        {
            return dataReader.GetDateTime(ordinal);
        }

        public override decimal GetDecimal(int ordinal)
        {
            return dataReader.GetDecimal(ordinal);
        }

        public override double GetDouble(int ordinal)
        {
            return dataReader.GetDouble(ordinal);
        }

        public override IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override Type GetFieldType(int ordinal)
        {
            return dataReader.GetFieldType(ordinal);
        }

        public override float GetFloat(int ordinal)
        {
            return dataReader.GetFloat(ordinal);
        }

        public override Guid GetGuid(int ordinal)
        {
            return dataReader.GetGuid(ordinal);
        }

        public override short GetInt16(int ordinal)
        {
            return dataReader.GetInt16(ordinal);
        }

        public override int GetInt32(int ordinal)
        {
            return dataReader.GetInt32(ordinal);
        }

        public override long GetInt64(int ordinal)
        {
            return dataReader.GetInt64(ordinal);
        }

        public override string GetName(int ordinal)
        {
            return dataReader.GetName(ordinal);
        }

        public override int GetOrdinal(string name)
        {
            return dataReader.GetOrdinal(name);
        }

        public override string GetString(int ordinal)
        {
            return dataReader.GetString(ordinal);
        }

        public override object GetValue(int ordinal)
        {
            return dataReader.GetValue(ordinal);
        }

        public override int GetValues(object[] values)
        {
            return dataReader.GetValues(values);
        }

        public override bool IsDBNull(int ordinal)
        {
            return dataReader.IsDBNull(ordinal);
        }

        public override bool NextResult()
        {
            return dataReader.NextResult();
        }

        public override bool Read()
        {
            return dataReader.Read();
        }
    }
}

    