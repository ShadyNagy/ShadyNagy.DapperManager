using System;
using ShadyNagy.DapperManager.Interfaces;
using ShadyNagy.DapperManager.Tests.Helpers;
using Shouldly;
using Xunit;

namespace ShadyNagy.DapperManager.Tests.OracleSyntaxBuilderTests
{
  public class ResetTest
  {
    private DiOracleHelper _diOracleHelper = DiOracleHelper.Create();

    [Fact]
    public void ReturnsEmptyString()
    {
      var oracleSyntaxBuilder = _diOracleHelper.GetService<ISyntaxBuilder>();
      oracleSyntaxBuilder.Reset();
      oracleSyntaxBuilder.Build().ShouldBe(string.Empty);
    }
  }
}
