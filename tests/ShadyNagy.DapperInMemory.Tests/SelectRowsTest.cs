using ShadyNagy.DapperInMemory.Tests.Constants;
using System.Data;
using Xunit;
using Dapper;
using System.Threading.Tasks;
using Shouldly;
using ShadyNagy.DapperInMemory.Tests.Helpers;
using ShadyNagy.DapperInMemory.Tests.Entities;
using System.Linq;

namespace ShadyNagy.DapperInMemory.Tests
{
    public class SelectRowsTest
    {
        [Fact]
        public async Task SelectsRowsByAllColumnsSuccess()
        {
            await DatabaseHelper.CreateTestTableAsync();
            await DatabaseHelper.InsertTestRowAsync();

            var connection = new InMemoryConnection(DatabaseConstants.CONNECTION_STRING);
            connection.Open();

            var sql = @"SELECT * FROM EMPLOYEES;";
            var employees =  (await connection.QueryAsync<Employee>(sql, commandType: CommandType.Text)).ToList();
            employees.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task SelectsRowsBySomeColumnsSuccess()
        {
            await DatabaseHelper.CreateTestTableAsync();
            await DatabaseHelper.InsertTestRowAsync();

            var connection = new InMemoryConnection(DatabaseConstants.CONNECTION_STRING);
            connection.Open();

            var sql = @"SELECT ID, NAME FROM EMPLOYEES;";
            var employees = (await connection.QueryAsync<Employee>(sql, commandType: CommandType.Text)).ToList();
            employees.Count.ShouldBeGreaterThan(0);
            employees[0].Id.ShouldNotBe(0);
            employees[0].Name.ShouldNotBeNull();
        }

        [Fact]
        public async Task SelectsRowsByOneColumnSuccess()
        {
            await DatabaseHelper.CreateTestTableAsync();
            await DatabaseHelper.InsertTestRowAsync();

            var connection = new InMemoryConnection(DatabaseConstants.CONNECTION_STRING);
            connection.Open();

            var sql = @"SELECT ID FROM EMPLOYEES;";
            var employees = (await connection.QueryAsync<Employee>(sql, commandType: CommandType.Text)).ToList();
            employees.Count.ShouldNotBe(0);
            employees[0].Name.ShouldBeNull();
        }
    }
}
