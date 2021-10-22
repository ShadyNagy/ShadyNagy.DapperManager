using ShadyNagy.DapperInMemory.Tests.Constants;
using System.Data;
using Xunit;
using Dapper;
using System.Threading.Tasks;
using Shouldly;
using ShadyNagy.DapperInMemory.Tests.Helpers;

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
            var affectedCount =  await connection.ExecuteAsync(sql, commandType: CommandType.Text);
            affectedCount.ShouldBeGreaterThan(0);
        }
    }
}
