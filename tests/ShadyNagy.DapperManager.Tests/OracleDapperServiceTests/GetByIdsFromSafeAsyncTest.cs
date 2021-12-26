using System;
using System.Threading.Tasks;
using ShadyNagy.DapperManager.Interfaces;
using ShadyNagy.DapperManager.Models;
using ShadyNagy.DapperManager.Tests.Entities;
using ShadyNagy.DapperManager.Tests.Helpers;
using Shouldly;
using Xunit;

namespace ShadyNagy.DapperManager.Tests.GetFromAsyncTests
{
  public class GetByIdsFromSafeAsyncTest
  {
    private DiOracleHelper _diOracleHelper = DiOracleHelper.Create();

    [Fact]
    public async Task ReturnsListSuccessAsync()
    {
      await DatabaseHelper.CreateAllTestTablesAsync();
      await DatabaseHelper.InsertAllRowsTablesAsync();

      var tableName = "EMPLOYEES";
      var oracleDapperService = _diOracleHelper.GetService<IDapperService>();
      var employeeFilterByMap = new EmployeeFilterByMap();
      var employees = await oracleDapperService.GetByFilterFromSafeAsync<Employee>(tableName, employeeFilterByMap, true);
      employees.Count.ShouldBeGreaterThan(0);
    }
  }
}
