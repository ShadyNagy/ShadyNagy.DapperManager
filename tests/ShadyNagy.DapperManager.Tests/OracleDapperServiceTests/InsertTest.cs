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
    public class InsertTest
    {
        private DiHelper _diHelper = DiHelper.Create();

        [Fact]
        public async Task ReturnsListSuccessAsync()
        {
            await DatabaseHelper.CreateTestTableAsync();

            var tableName = "EMPLOYEES";
            var employee = new Employee()
            {
                Id = 1,
                Name = "Shady"
            };
            var oracleDapperService = _diHelper.GetService<IDapperService>();
            var affectedRows = oracleDapperService.Insert<Employee>(tableName, employee);
            affectedRows.ShouldBeGreaterThan(0);

            var employees = oracleDapperService.GetFrom<Employee>(tableName);
            employees.Count.ShouldBeGreaterThan(0);
        }
    }
}
