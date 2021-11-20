using ShadyNagy.DapperManager.Interfaces;
using ShadyNagy.DapperManager.Tests.Helpers;
using Shouldly;
using Xunit;

namespace ShadyNagy.DapperManager.Tests.OracleSyntaxBuilderTests
{
  public class SelectAllFromTest
  {
    private DiOracleHelper _diOracleHelper = DiOracleHelper.Create();

    [Fact]
    public void ReturnsSelectAllSyntaxString()
    {
      var tableName = "TABLE";
      var expected = $"SELECT * FROM {tableName}";

      var oracleSyntaxBuilder = _diOracleHelper.GetService<ISyntaxBuilder>();
      oracleSyntaxBuilder.SelectAllFrom(tableName);
      oracleSyntaxBuilder.Build().ShouldBe(expected);
    }
  }
}
