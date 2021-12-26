using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Dapper;
using ShadyNagy.DapperManager.Models;

namespace ShadyNagy.DapperManager.Extensions
{
  public static class ObjectExtensions
  {
    public static List<string> GetPropertiesName(this object obj)
    {
      var propertyList = new List<string>();
      if (obj == null)
      {
        return propertyList;
      }

      foreach (var prop in obj.GetType().GetProperties())
      {
        propertyList.Add(prop.Name);
      }

      return propertyList;
    }

    public static string GetPropertyNameByValue(this object obj, string internalValueToSearchFor)
    {
      var propertiesName = obj.GetPropertiesName();
      for (var i = 0; i < propertiesName.Count; i++)
      {
        var value = obj.GetPropertyValue(propertiesName[i]);
        var propertiesName2 = value.GetPropertiesName();
        for (var j = 0; j < propertiesName2.Count; j++)
        {
          var value2 = value.GetPropertyValue(propertiesName2[j]);
          if (value2 == null)
          {
            continue;
          }

          if (internalValueToSearchFor.ToLower() == value2.ToString().ToLower())
          {
            return propertiesName[i];
          }
        }
      }

      return null;
    }

    public static string GetPropertyName(this object obj, string propertyNameIgnoreCase)
    {
      if (obj == null)
      {
        return null;
      }

      foreach (var prop in obj.GetType().GetProperties())
      {
        if (string.Equals(propertyNameIgnoreCase, prop.Name, StringComparison.CurrentCultureIgnoreCase))
        {
          return prop.Name;
        }
      }

      return null;
    }

    public static Type GetPropertyType(this object obj, string objectPropertyName)
    {
      if (string.IsNullOrEmpty(objectPropertyName))
      {
        return null;
      }

      var prop = obj.GetType().GetProperty(objectPropertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

      return prop?.PropertyType;
    }

    public static object SetPropertyValueByName(this object obj, string propertyName, object value, string format = null)
    {
      var propertyInfo = obj.GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

      if (propertyInfo == null)
      {
        return obj;
      }
      var propertyType = obj.GetPropertyType(propertyName);

      var val = value is DBNull ? null : value;
      if (val == null)
      {
        propertyInfo.SetValue(obj, null, null);
        return obj;
      }

      if (propertyType == typeof(long) || propertyType == typeof(long?))
      {
        long.TryParse(val.ToString(), out var parsedValue);
        propertyInfo.SetValue(obj, parsedValue, null);
        return obj;
      }
      else if (propertyType == typeof(int) || propertyType == typeof(int?))
      {
        int.TryParse(val.ToString(), out var parsedValue);
        propertyInfo.SetValue(obj, parsedValue, null);
        return obj;
      }
      else if (propertyType == typeof(double) || propertyType == typeof(double?))
      {
        double.TryParse(val.ToString(), out var parsedValue);
        propertyInfo.SetValue(obj, parsedValue, null);
        return obj;
      }
      else if (propertyType == typeof(decimal) || propertyType == typeof(decimal?))
      {
        decimal.TryParse(val.ToString(), out var parsedValue);
        propertyInfo.SetValue(obj, parsedValue, null);
        return obj;
      }
      else if (propertyType == typeof(char) || propertyType == typeof(char?))
      {
        char.TryParse(val.ToString(), out var parsedValue);
        propertyInfo.SetValue(obj, parsedValue, null);
        return obj;
      }
      else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
      {
        if (string.IsNullOrEmpty(format))
        {
          DateTime.TryParse(val.ToString(), out var parsedValue);
          propertyInfo.SetValue(obj, parsedValue, null);
        }
        else
        {
          DateTime parsedValue;
          if (val is string)
          {
            DateTime.TryParseExact((string)val, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedValue);
          }
          else
          {
            DateTime.TryParseExact(((DateTime)val).ToString(format), format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedValue);
          }
          propertyInfo.SetValue(obj, parsedValue, null);
        }

        return obj;
      }

      if (value is DateTime && !string.IsNullOrEmpty(format))
      {
        val = ((DateTime)value).ToString(format);
      }

      propertyInfo.SetValue(obj, val, null);

      return obj;
    }
    public static Object GetPropertyValue(this Object obj, string objectPropertyName)
    {
      if (string.IsNullOrEmpty(objectPropertyName))
      {
        return null;
      }

      var prop = obj.GetType().GetProperty(objectPropertyName,
          BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

      if (prop != null && prop.CanRead)
      {
        return prop.GetValue(obj);
      }

      return null;
    }

    public static DynamicParameters ToDynamicParameters(this object databaseFields)
    {
      var properties = databaseFields.GetPropertiesName();
      var parameters = new DynamicParameters();
      foreach (var property in properties)
      {
        var value = databaseFields.GetPropertyValue(property) as DatabaseField;
        if (value == null)
        {
          continue;
        }
        if (value.Value == null || value.Value.ToString() == "null")
        {
          parameters.Add($"@{property}");
        }
        else
        {
          parameters.Add($"@{property}", value);
        }
      }

      return parameters;
    }
  }
}
