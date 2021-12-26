using ShadyNagy.DapperManager.Models;

namespace ShadyNagy.DapperManager.Tests.Entities
{
  class EmployeeFilterByMap
  {
    public DatabaseField Id { get; set; } = new DatabaseField(DatabaseFieldDirection.In, "Id", DatabaseFieldType.Varchar2, 1);
    public DatabaseField Name { get; set; } = new DatabaseField(DatabaseFieldDirection.In, "Name", DatabaseFieldType.Varchar2, null);
  }
}
