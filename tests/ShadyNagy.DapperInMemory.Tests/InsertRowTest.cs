using System.Data;
using System.Threading.Tasks;
using Dapper;
using ShadyNagy.DapperInMemory.Tests.Constants;
using ShadyNagy.DapperInMemory.Tests.Helpers;
using Shouldly;
using Xunit;

namespace ShadyNagy.DapperInMemory.Tests
{
  public class InsertRowTest
  {
    [Fact]
    public async Task InsertsRowSuccess()
    {
      await DatabaseHelper.CreateTestTableAsync();

      var connection = new InMemoryConnection(DatabaseConstants.CONNECTION_STRING);
      connection.Open();

      var sql = @"
INSERT INTO EMPLOYEES
(ID, NAME)
VALUES
(1, 'Shady');
";
      var affectedCount = await connection.ExecuteAsync(sql, commandType: CommandType.Text);
      affectedCount.ShouldBeGreaterThan(0);
    }
  }
}
