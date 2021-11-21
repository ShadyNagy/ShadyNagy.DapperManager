using System.Threading.Tasks;
using ShadyNagy.DapperManager.Interfaces;
using ShadyNagy.DapperManager.Tests.Helpers;
using Shouldly;
using Xunit;

namespace ShadyNagy.DapperManager.Tests.OracleDapperInfoServiceTests
{
  public class GetAllTablesNamesAsyncTest
  {
    private DiOracleHelper _diOracleHelper = DiOracleHelper.Create();

    [Fact]
    public async Task ReturnsTablesNamesListSuccessAsync()
    {
      await DatabaseHelper.CreateAllTestTablesAsync();
      await DatabaseHelper.InsertAllRowsTablesAsync();

      var oracleDapperInfoService = _diOracleHelper.GetService<IDapperInfoService>();
      var tablesNames = await oracleDapperInfoService.GetAllTablesNamesAsync();
      tablesNames.Count.ShouldBeGreaterThan(0);
    }
  }
}
