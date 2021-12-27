using ShadyNagy.DapperManager.Models;

namespace ShadyNagy.DapperManager.Tests.Entities
{
  class EmployeeFilterByMap
  {
    public DatabaseMapField Id { get; set; } = new DatabaseMapField(DatabaseMapFieldDirection.In, "Id", DatabaseMapFieldType.Varchar2, 1);
    public DatabaseMapField Name { get; set; } = new DatabaseMapField(DatabaseMapFieldDirection.In, "Name", DatabaseMapFieldType.Varchar2, null);
  }
}
