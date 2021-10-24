using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ShadyNagy.DapperInMemory
{
    internal class InMemoryCommand : IDbCommand
    {
        private DataSet _currentDataSet = new DataSet();

        public InMemoryCommand(Dictionary<string, DataSet> dataSets)
        {     
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
            if (CommandType == CommandType.Text)
            {
                if (GetSqlCommandType() == SqlSyntexType.Nothing)
                {
                    return 0;
                }
                else if (GetSqlCommandType() == SqlSyntexType.Select)
                {
                    return 0;
                }
                else if (GetSqlCommandType() == SqlSyntexType.Insert)
                {
                    return ExcuteInsert();
                }
                else if (GetSqlCommandType() == SqlSyntexType.CreateTable)
                {
                    return ExcuteCreateTable();
                }
            }

            return 0;
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
            return null;
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
            else if (parts[0].ToLower() == "create" && parts[1].ToLower() == "table")
            {
                return SqlSyntexType.CreateTable;
            }

            return SqlSyntexType.Nothing;
        }

        private string GetSelectTableName()
        {
            var fromIndex = CommandText.IndexOf("FROM");
            var fromString = CommandText.Substring(fromIndex);
            var parts = fromString.Split(" ");
            if (parts.Length <= 1)
            {
                return string.Empty;
            }

            return parts[1];
        }

        private string GetInsertTableName()
        {
            var parts = GetCommandTextParts();
            if (parts.Length <= 2)
            {
                return string.Empty;
            }

            return parts[2];
        }

        private string GetCreateTableName()
        {
            var parts = GetCommandTextParts();
            if (parts.Length <= 2)
            {
                return string.Empty;
            }

            return parts[2];
        }

        
        private string[] GetSelectTableColumns()
        {
            var selectIndex = CommandText.IndexOf("SELECT");
            var columnsIndex = selectIndex + 6;
            var fromIndex = CommandText.IndexOf("FROM");
            var columnsString =  CommandText.Substring(columnsIndex, fromIndex - columnsIndex).Trim();

            var columns = columnsString.Split(",");
            for (var i = 0; i < columns.Length; i++)
            {
                columns[i] = columns[i].TrimStart(' ').TrimEnd(' ');
            }

            return columns;
        }

        private string[] GetInsertColumns(string tableName)
        {
            var parts = CommandText.Split(tableName);
            if (parts.Length <= 1)
            {
                return new string[0];
            }

            var partsSub = parts[1].Split("VALUES");
            var columnsString = partsSub[0]
                .Replace(" ", string.Empty)
                .Replace("(", string.Empty)
                .Replace(")", string.Empty);

            return columnsString.Split(",");
        }

        private DataColumn[] GetCreateTableColumns(string tableName)
        {
            var result = new List<DataColumn>();

            var parts = CommandText.Split(tableName);
            if (parts.Length <= 1)
            {
                return result.ToArray();
            }
            
            var partsSub = parts[1].Split(",");
            for (var i = 0; i < partsSub.Length; i++)
            {
                var column = new DataColumn();
                var columnData = partsSub[i]
                    .Replace(";", string.Empty)
                    .Replace("(", string.Empty)
                    .Replace(")", string.Empty)
                    .TrimStart()
                    .TrimEnd();

                var columnParts = columnData.Split(" ");
                for (var c = 0; c < 3; c++)
                {
                    if (string.IsNullOrEmpty(column.ColumnName))
                    {
                        column.ColumnName = columnParts[c];
                    }
                    else if (c == 1)
                    {
                        column.DataType = GetDataType(columnParts[c]);
                    }
                    else
                    {
                        column.AllowDBNull = columnParts[c].ToUpper() == "NULL" ? true: false;
                    }                        
                }

                result.Add(column);
            }

            return result.ToArray();
        }        

        private Type GetDataType(string type)
        {
            if (type.ToUpper().Contains("VARCHAR2"))
            {
                return typeof(string);
            }
            else if (type.ToUpper().Contains("NUMBER"))
            {
                return typeof(int);
            }

            return typeof(string);
        }

        private string[] GetInsertValues(string tableName)
        {
            var parts = CommandText.Split(tableName);
            if (parts.Length <= 1)
            {
                return new string[1];
            }

            var partsSub = parts[1].Split("VALUES");
            partsSub[1] = Between(partsSub[1], "(", ")");
            var values = partsSub[1]
                .Replace(" ", string.Empty)
                .Replace(";", string.Empty);

            return values.Split(",");
        }

        private string Between(string data, string FirstString, string LastString)
        {
            var pos1 = data.IndexOf(FirstString) + FirstString.Length;
            var pos2 = data.IndexOf(LastString);
            var finalString = data.Substring(pos1, pos2 - pos1);

            return finalString;
        }

        private IDataReader? ExcuteSelect()
        {
            var tableName = GetSelectTableName();

            if (!_currentDataSet.Tables.Contains(tableName))
            {
                return null;
            }
            var columns = GetSelectTableColumns();
            var table = _currentDataSet.Tables[tableName];
            if (columns.Length > 0 && columns[0] != "*")
            {
                DataColumn[] tableColumns = new DataColumn[table.Columns.Count];
                table.Columns.CopyTo(tableColumns, 0);
                foreach (DataColumn column in tableColumns)
                {
                    if (!columns.Contains(column.ColumnName))
                    {
                        table.Columns.Remove(column.ColumnName);
                    }
                }
            }
            
            return table.CreateDataReader();
        }

        private int ExcuteInsert()
        {
            var addedRows = 0;
            var tableName = GetInsertTableName();

            if (!_currentDataSet.Tables.Contains(tableName))
            {
                return addedRows;
            }
            var table = _currentDataSet.Tables[tableName];

            var columns = GetInsertColumns(tableName);
            if (columns.Length <= 0)
            {
                return addedRows;
            }
            var values = GetInsertValues(tableName);
            if (values.Length <= 0)
            {
                return addedRows;
            }

            DataRow row = table.NewRow();
            for (var i = 0; i < columns.Length; i++)
            {
                row[columns[i]] = values[i];                
            }
            table.Rows.Add(row);
            addedRows++;

            return addedRows;
        }

        private int ExcuteCreateTable()
        {
            var affectedCount = 0;
            var tableName = GetCreateTableName();

            if (_currentDataSet.Tables.Contains(tableName))
            {
                _currentDataSet.Tables.Remove(tableName);
            }
            var columns = GetCreateTableColumns(tableName);
            if (columns.Length <= 0)
            {
                return affectedCount;
            }

            var table = _currentDataSet.Tables.Add(tableName);
            for (var i = 0; i < columns.Length; i++)
            {
                table.Columns.Add(columns[i]);
            }

            affectedCount = 1;

            return affectedCount;
        }
    }
}
