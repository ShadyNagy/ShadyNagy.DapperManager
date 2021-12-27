using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ShadyNagy.DapperManager.Extensions;
using ShadyNagy.DapperManager.Interfaces;
using ShadyNagy.DapperManager.Models;

namespace ShadyNagy.DapperManager.Microsoft
{
  public class MicrosoftSyntaxBuilder : ISyntaxBuilder
  {
    private const string SELECT = "SELECT";
    private const string FROM = "FROM";
    private const string ALL = "*";
    private const string INSERT = "INSERT INTO";
    private const string UPDATE = "UPDATE";
    private const string SET = "SET";
    private const string WHERE = "WHERE";
    private const string AND = "AND";

    public StringBuilder Syntax { get; private set; } = new StringBuilder();

    public ISyntaxBuilder Reset()
    {
      Syntax = new StringBuilder();

      return this;
    }

    public ISyntaxBuilder SelectAllFrom(string name)
    {
      this
          .Select()
          .All()
          .From(name);

      return this;
    }

    public ISyntaxBuilder SelectByFrom(string tableFullName, Dictionary<string, object> keys)
    {
      this
        .Select()
        .All()
        .From(tableFullName)
        .Where(keys);

      return this;
    }

    public ISyntaxBuilder SelectByFromSafe(string tableFullName, Dictionary<string, string> keys)
    {
      this
        .Select()
        .All()
        .From(tableFullName)
        .WhereSafe(keys);

      return this;
    }

    public ISyntaxBuilder SelectByFromSafe(string tableFullName, object databaseFields)
    {
      this
        .Select()
        .All()
        .From(tableFullName)
        .WhereSafe(databaseFields);

      return this;
    }

    public ISyntaxBuilder Insert(string tableFullName, object obj)
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
          .AddInsertValues(values);

      return this;
    }

    public ISyntaxBuilder Update(string tableFullName, Dictionary<string, object> keys, object obj)
    {
      if (obj == null)
      {
        return this;
      }

      var properties = GetPropertiesNames(obj);
      var values = GetPropertiesValues(properties, obj);
      this
        .Update(tableFullName)
        .Set(properties, values)
        .Where(keys);

      return this;
    }

    public ISyntaxBuilder InsertSafe(string tableFullName, object obj, Dictionary<string, string> mapper = null)
    {
      if (obj == null)
      {
        return this;
      }

      if (mapper == null)
      {
        var properties = GetPropertiesNames(obj);
        var values = GetPropertiesValues(properties, obj);
        this
          .Insert(tableFullName)
          .AddInsertColumns(properties)
          .AddInsertValues(values);
      }
      else
      {
        this
          .Insert(tableFullName)
          .AddInsertColumns(mapper.Keys.ToArray())
          .AddInsertSafeValues(mapper.Values.ToArray());
      }

      return this;
    }

    public ISyntaxBuilder UpdateSafe(string tableFullName, object obj, Dictionary<string, string> keys, Dictionary<string, string> mapper)
    {
      if (obj == null)
      {
        return this;
      }

      this
        .Update(tableFullName)
        .SetSafe(mapper.Keys.ToArray(), mapper.Values.ToArray())
        .WhereSafe(keys);

      return this;
    }

    public ISyntaxBuilder SelectColumnsFrom(string name, List<string> columnsNames)
    {
      this
          .Select(columnsNames)
          .From(name);

      return this;
    }

    public ISyntaxBuilder All()
    {
      Syntax = Syntax.Append($"{ALL} ");

      return this;
    }

    public ISyntaxBuilder Select()
    {
      Syntax = new StringBuilder($"{SELECT} ");

      return this;
    }

    public ISyntaxBuilder Insert(string tableFullName)
    {
      Syntax = new StringBuilder($"{INSERT} {tableFullName} ");

      return this;
    }

    public ISyntaxBuilder Update(string tableFullName)
    {
      Syntax = new StringBuilder($"{UPDATE} {tableFullName} ");

      return this;
    }

    public ISyntaxBuilder WhereSafe(object databaseFields)
    {
      Syntax.Append($" {WHERE} ");

      var whereValues = new List<string>();
      var properties = databaseFields.GetPropertiesName();
      foreach (var property in properties)
      {
        var value = databaseFields.GetPropertyValue(property) as DatabaseMapField;
        if (value == null)
        {
          continue;
        }

        var valueType = value.GetPropertyType("Value");
        if (valueType.IsGenericType && (valueType.GetGenericTypeDefinition() == typeof(List<>) || valueType.IsArray))
        {
          whereValues.Add($"{value.FieldName} IN @{property}");
        }
        else
        {
          whereValues.Add($"{value.FieldName}=@{property}");
        }
        
      }
      Syntax.Append(string.Join($" {AND} ", whereValues.ToArray()));

      return this;
    }

    public ISyntaxBuilder Set(string[] columnsNames, object[] values)
    {
      Syntax.Append($"{SET} ");

      var columnsValues = new List<string>();
      for (var i = 0; i < columnsNames.Length; i++)
      {
        if (i >= values.Length)
        {
          break;
        }
        columnsValues.Add($"{columnsNames[i]}={values[i]}");
      }
      Syntax.Append(string.Join(",", columnsValues.ToArray()));

      return this;
    }

    public ISyntaxBuilder SetSafe(string[] columnsNames, string[] parametersNames)
    {
      Syntax.Append($"{SET} ");

      var columnsValues = new List<string>();
      for (var i = 0; i < columnsNames.Length; i++)
      {
        if (i >= parametersNames.Length)
        {
          break;
        }
        columnsValues.Add($"{columnsNames[i]}=@{parametersNames[i]}");
      }
      Syntax.Append(string.Join(",", columnsValues.ToArray()));

      return this;
    }

    public ISyntaxBuilder Where(Dictionary<string, object> keys)
    {
      Syntax.Append($" {WHERE} ");

      var whereValues = new List<string>();
      foreach (var key in keys)
      {
        whereValues.Add($"{key.Key}={key.Value}");

      }
      Syntax.Append(string.Join($" {AND} ", whereValues.ToArray()));

      return this;
    }

    public ISyntaxBuilder WhereSafe(Dictionary<string, string> keys)
    {
      Syntax.Append($" {WHERE} ");

      var whereValues = new List<string>();
      foreach (var key in keys)
      {
        whereValues.Add($"{key.Key}=@{key.Value}");
        
      }
      Syntax.Append(string.Join($" {AND} ", whereValues.ToArray()));

      return this;
    }

    public ISyntaxBuilder Select(List<string> columnsNames)
    {
      Syntax = new StringBuilder($"{SELECT} ");
      Syntax.Append(string.Join(",", columnsNames.ToArray()));
      Syntax.Append(" ");

      return this;
    }

    public ISyntaxBuilder From(string name)
    {
      Syntax.Append($"{FROM} {name}");

      return this;
    }

    public ISyntaxBuilder Columns(string name)
    {
      Syntax.Append($"{FROM} {name}");

      return this;
    }

    public ISyntaxBuilder AddInsertColumns(string[] columns)
    {
      var result = new StringBuilder("(");
      result.Append(string.Join(",", columns));
      result.Append(") ");

      Syntax.Append(result);

      return this;
    }

    public ISyntaxBuilder AddInsertValues(object[] values)
    {
      var result = new StringBuilder("VALUES (");

      for (int i = 0; i < values.Length; i++)
      {
        result.Append(values[i]);
        result.Append(",");
      }
      result = new StringBuilder(result.ToString().Substring(0, result.Length - 1));

      result.Append(")");

      Syntax.Append(result);

      return this;
    }

    public ISyntaxBuilder AddInsertSafeValues(string[] properties)
    {
      var result = new StringBuilder("VALUES (");

      for (int i = 0; i < properties.Length; i++)
      {
        result.Append("@");
        result.Append(properties[i]);
        result.Append(",");
      }
      result = new StringBuilder(result.ToString().Substring(0, result.Length - 1));

      result.Append(")");

      Syntax.Append(result);

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
      return Syntax.ToString();
    }
  }
}
