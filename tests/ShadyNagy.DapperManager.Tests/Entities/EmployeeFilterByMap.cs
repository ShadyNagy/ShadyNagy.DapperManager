using ShadyNagy.DapperManager.Models;

namespace ShadyNagy.DapperManager.Tests.Entities
{
  class EmployeeFilterByMap
  {
    public DatabaseMapField Id { get; set; } = new DatabaseMapField(DatabaseFieldDirection.In, "Id", DatabaseFieldType.Varchar2, 1);
    public DatabaseMapField Name { get; set; } = new DatabaseMapField(DatabaseFieldDirection.In, "Name", DatabaseFieldType.Varchar2, null);
  }
}
