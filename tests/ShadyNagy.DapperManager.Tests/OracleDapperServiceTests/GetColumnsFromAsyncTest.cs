using System;
using System.Linq;
using System.Threading.Tasks;
using ShadyNagy.DapperManager.Interfaces;
using ShadyNagy.DapperManager.Tests.Entities;
using ShadyNagy.DapperManager.Tests.Helpers;
using Shouldly;
using Xunit;

namespace ShadyNagy.DapperManager.Tests.GetFromAsyncTests
{
  public class GetColumnsFromAsyncTest
  {
    private DiOracleHelper _diOracleHelper = DiOracleHelper.Create();

    [Fact]
    public async Task ReturnsListSuccessAsync()
    {
      await DatabaseHelper.CreateTestTableAsync();
      await DatabaseHelper.InsertTestRowAsync();

      var tableName = "EMPLOYEES";
      var columns = new string[] { "ID" };
      var oracleDapperService = _diOracleHelper.GetService<IDapperService>();
      var employees = await oracleDapperService.GetColumnsFromAsync<Employee>(tableName, columns.ToList());
      employees.Count.ShouldBeGreaterThan(0);
      employees[0].Id.ShouldNotBe(0);
      employees[0].Name.ShouldBeNull();
    }
  }
}
