using ShadyNagy.DapperManager.Interfaces;
using ShadyNagy.DapperManager.Oracle;
using ShadyNagy.DapperManager.Tests.Entities;
using ShadyNagy.DapperManager.Tests.Helpers;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ShadyNagy.DapperManager.Tests.GetFromAsyncTests
{
    public class GetColumnsFromTest
    {
        private DiHelper _diHelper = DiHelper.Create();

        [Fact]
        public async Task ReturnsListSuccessAsync()
        {
            await DatabaseHelper.CreateTestTableAsync();
            await DatabaseHelper.InsertTestRowAsync();

            var tableName = "EMPLOYEES";
            var columns = new string[] { "ID" };
            var oracleDapperService = _diHelper.GetService<IDapperService>();
            var employees = oracleDapperService.GetColumnsFrom<Employee>(tableName, columns.ToList());
            employees.Count.ShouldBeGreaterThan(0);
        }
    }
}
