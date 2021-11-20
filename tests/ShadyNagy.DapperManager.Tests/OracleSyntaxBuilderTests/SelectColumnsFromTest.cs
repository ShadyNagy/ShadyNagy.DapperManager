using System.Linq;
using ShadyNagy.DapperManager.Interfaces;
using ShadyNagy.DapperManager.Oracle;
using ShadyNagy.DapperManager.Tests.Helpers;
using Shouldly;
using Xunit;

namespace ShadyNagy.DapperManager.Tests.OracleSyntaxBuilderTests
{
  public class SelectColumnsFromTest
  {
    private DiOracleHelper _diOracleHelper = DiOracleHelper.Create();

    [Fact]
    public void ReturnsSelectAllSyntaxString()
    {
      var tableName = "TABLE";
      var columnsNames = new string[] { "EMPLOYEES", "COUNTRIES" }.ToList();
      var expected = $"SELECT EMPLOYEES,COUNTRIES FROM {tableName}";


      var oracleSyntaxBuilder = _diOracleHelper.GetService<ISyntaxBuilder>();
      oracleSyntaxBuilder.SelectColumnsFrom(tableName, columnsNames);
      oracleSyntaxBuilder.Build().ShouldBe(expected);
    }
  }
}
