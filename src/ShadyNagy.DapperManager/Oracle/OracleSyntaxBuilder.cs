﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ShadyNagy.DapperManager.Interfaces;

namespace ShadyNagy.DapperManager.Oracle
{
  public class OracleSyntaxBuilder : ISyntaxBuilder
  {
    private const string SELECT = "SELECT";
    private const string FROM = "FROM";
    private const string ALL = "*";
    private const string INSERT = "INSERT INTO";

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
          .AddInserValues(values);

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

    public ISyntaxBuilder AddInserValues(object[] values)
    {
      var result = new StringBuilder("VALUES (");

      for (int i = 0; i < values.Length; i++)
      {
        result.Append(values[i].ToString());
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