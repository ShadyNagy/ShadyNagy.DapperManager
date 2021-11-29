﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ShadyNagy.DapperManager.Interfaces;

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

    public ISyntaxBuilder Update(string tableFullName, object obj)
    {
      if (obj == null)
      {
        return this;
      }

      var properties = GetPropertiesNames(obj);
      var values = GetPropertiesValues(properties, obj);
      this
        .Update(tableFullName)
        .Set(properties, values);

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

    public ISyntaxBuilder UpdateSafe(string tableFullName, object obj, Dictionary<string, string> mapper = null)
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
          .Update(tableFullName)
          .Set(properties, values);
      }
      else
      {
        this
          .Update(tableFullName)
          .SetSafe(mapper.Keys.ToArray(), mapper.Values.ToArray());
      }

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
