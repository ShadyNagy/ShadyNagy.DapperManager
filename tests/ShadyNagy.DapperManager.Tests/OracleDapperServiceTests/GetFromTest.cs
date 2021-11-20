using System;
using System.Threading.Tasks;
using ShadyNagy.DapperManager.Interfaces;
using ShadyNagy.DapperManager.Oracle;
using ShadyNagy.DapperManager.Tests.Entities;
using ShadyNagy.DapperManager.Tests.Helpers;
using Shouldly;
using Xunit;

namespace ShadyNagy.DapperManager.Tests.GetFromAsyncTests
{
  public class GetFromTest
  {
    private DiOracleHelper _diOracleHelper = DiOracleHelper.Create();

    [Fact]
    public async Task ReturnsListSuccessAsync()
    {
      await DatabaseHelper.CreateTestTableAsync();
      await DatabaseHelper.InsertTestRowAsync();

      var tableName = "EMPLOYEES";
      var oracleDapperService = _diOracleHelper.GetService<IDapperService>();
      var employees = oracleDapperService.GetFrom<Employee>(tableName);
      employees.Count.ShouldBeGreaterThan(0);
    }
  }
}
