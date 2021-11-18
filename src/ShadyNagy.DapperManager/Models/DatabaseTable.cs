using System.Collections.Generic;

namespace ShadyNagy.DapperManager.Models
{
  public class DatabaseTable
  {
    public string Schema { get; set; }
    public string Name { get; set; }
    public string TableType { get; set; }
    public string Catalog { get; set; }
    public List<DatabaseColumn> Columns { get; set; } = new List<DatabaseColumn>();
  }
}
