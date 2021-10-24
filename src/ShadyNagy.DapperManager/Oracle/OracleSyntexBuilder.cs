using ShadyNagy.DapperManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ShadyNagy.DapperManager.Oracle
{
    public class OracleSyntexBuilder : ISyntexBuilder
    {
        private const string SELECT = "SELECT";
        private const string FROM = "FROM";
        private const string ALL = "*";
        private const string INSERT = "INSERT INTO";

        public StringBuilder Syntex { get; private set; } = new StringBuilder();

        public ISyntexBuilder Reset()
        {
            Syntex = new StringBuilder();

            return this;
        }

        public ISyntexBuilder SelectAllFrom(string name)
        {
            this
                .Select()
                .All()
                .From(name);

            return this;
        }

        public ISyntexBuilder Insert(string tableFullName, object obj)
        {
            if (obj == null)
            {
                return this;
            }

            var properties = GetPropertiesNames(obj);
            var values = GetPropertiesValues(properties, obj);
            this
                .Insert(tableFullName)
                .AddInsertColumns(properties)
                .AddInserValues(values);

            return this;
        }

        public ISyntexBuilder SelectColumnsFrom(string name, List<string> columnsNames)
        {
            this
                .Select(columnsNames)
                .From(name);

            return this;
        }

        public ISyntexBuilder All()
        {
            Syntex = Syntex.Append($"{ALL} ");

            return this;
        }

        public ISyntexBuilder Select()
        {
            Syntex = new StringBuilder($"{SELECT} ");

            return this;
        }

        public ISyntexBuilder Insert(string tableFullName)
        {
            Syntex = new StringBuilder($"{INSERT} {tableFullName} ");

            return this;
        }

        public ISyntexBuilder Select(List<string> columnsNames)
        {
            Syntex = new StringBuilder($"{SELECT} ");
            Syntex.Append(string.Join(",", columnsNames.ToArray()));
            Syntex.Append(" ");

            return this;
        }

        public ISyntexBuilder From(string name)
        {
            Syntex.Append($"{FROM} {name}");

            return this;
        }

        public ISyntexBuilder Columns(string name)
        {
            Syntex.Append($"{FROM} {name}");

            return this;
        }

        public ISyntexBuilder AddInsertColumns(string[] columns)
        {
            var result = new StringBuilder("(");
            result.Append(string.Join(",", columns));
            result.Append(") ");

            Syntex.Append(result);

            return this;
        }

        public ISyntexBuilder AddInserValues(object[] values)
        {
            var result = new StringBuilder("VALUES (");

            for (int i = 0; i < values.Length; i++)
            {
                result.Append(values[i].ToString());
                result.Append(",");
            }
            result = new StringBuilder(result.ToString().Substring(0, result.Length - 1));

            result.Append(")");

            Syntex.Append(result);

            return this;
        }

        private object[] GetPropertiesValues<T>(string[] propertiesName, T obj) 
        {
            var result = new List<object>();

            for (int i = 0; i < propertiesName.Length; i++)
            {
                var value = obj.GetType().GetProperty(propertiesName[i]).GetValue(obj);
                result.Add(value);
            }

            return result.ToArray();
        }

        private string[] GetPropertiesNames(object obj)
        {
            var propertyInfos = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            return propertyInfos.Select(x => x.Name).ToArray();
        }                

        public string Build()
        {
            return Syntex.ToString();
        }
    }
}
