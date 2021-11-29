using System.Collections.Generic;
using System.Data;

namespace ShadyNagy.DapperManager.Interfaces
{
  public interface ISyntaxBuilder
  {
    string Build();
    ISyntaxBuilder Reset();
    ISyntaxBuilder SelectAllFrom(string name);
    ISyntaxBuilder SelectColumnsFrom(string name, List<string> columnsNames);
    ISyntaxBuilder All();
    ISyntaxBuilder Select();
    ISyntaxBuilder Select(List<string> columnsNames);
    ISyntaxBuilder From(string name);
    ISyntaxBuilder Insert(string tableFullName, object obj);
    ISyntaxBuilder InsertSafe(string tableFullName, object obj, Dictionary<string, string> mapper);
    ISyntaxBuilder Update(string tableFullName);
    ISyntaxBuilder Where(Dictionary<string, object> keys);
    ISyntaxBuilder WhereSafe(Dictionary<string, string> keys);
    ISyntaxBuilder Update(string tableFullName, Dictionary<string, object> keys, object obj);
    ISyntaxBuilder UpdateSafe(string tableFullName, object obj, Dictionary<string, string> keys, Dictionary<string, string> mapper);
    ISyntaxBuilder Set(string[] columnsNames, object[] values);
    ISyntaxBuilder SetSafe(string[] columnsNames, string[] parametersNames);
    ISyntaxBuilder AddInsertColumns(string[] columns);
    ISyntaxBuilder AddInsertValues(object[] values);
    ISyntaxBuilder AddInsertSafeValues(string[] properties);
  }
}
