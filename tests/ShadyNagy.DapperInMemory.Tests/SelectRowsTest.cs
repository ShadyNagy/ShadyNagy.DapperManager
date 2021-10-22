using ShadyNagy.DapperInMemory.Tests.Constants;
using System.Data;
using Xunit;
using Dapper;
using System.Threading.Tasks;
using Shouldly;
using ShadyNagy.DapperInMemory.Tests.Helpers;
using ShadyNagy.DapperInMemory.Tests.Models;
using System.Linq;

namespace ShadyNagy.DapperInMemory.Tests
{
    public class SelectRowsTest
    {
        [Fact]
        public async Task SelectsRowsSuccess()
        {
            await DatabaseHelper.CreateTestTableAsync();
            await DatabaseHelper.InsertTestRowAsync();

            var connection = new InMemoryConnection(DatabaseConstants.CONNECTION_STRING);
            connection.Open();

            var sql = @"SELECT * FROM EMPLOYEES;";
            var employees =  (await connection.QueryAsync<Employee>(sql, commandType: CommandType.Text)).ToList();
            employees.Count.ShouldBeGreaterThan(0);
        }
    }
}
