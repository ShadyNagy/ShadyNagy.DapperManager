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
    ISyntaxBuilder AddInsertColumns(string[] columns);
    ISyntaxBuilder AddInsertValues(object[] values);
  }
}
