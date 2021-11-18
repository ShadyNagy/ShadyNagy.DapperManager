using System;
using ShadyNagy.DapperManager.Interfaces;
using ShadyNagy.DapperManager.Oracle;
using ShadyNagy.DapperManager.Tests.Helpers;
using Shouldly;
using Xunit;

namespace ShadyNagy.DapperManager.Tests.OracleSyntaxBuilderTests
{
  public class ResetTest
  {
    private DiHelper _diHelper = DiHelper.Create();

    [Fact]
    public void ReturnsEmptyString()
    {
      var oracleSyntaxBuilder = _diHelper.GetService<ISyntaxBuilder>();
      oracleSyntaxBuilder.Reset();
      oracleSyntaxBuilder.Build().ShouldBe(string.Empty);
    }
  }
}
