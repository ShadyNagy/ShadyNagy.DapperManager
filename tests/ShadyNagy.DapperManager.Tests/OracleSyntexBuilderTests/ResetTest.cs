using ShadyNagy.DapperManager.Interfaces;
using ShadyNagy.DapperManager.Oracle;
using ShadyNagy.DapperManager.Tests.Helpers;
using Shouldly;
using System;
using Xunit;

namespace ShadyNagy.DapperManager.Tests.OracleSyntexBuilderTests
{
    public class ResetTest
    {
        private DiHelper _diHelper = DiHelper.Create();

        [Fact]
        public void ReturnsEmptyString()
        {
            var oracleSyntexBuilder = _diHelper.GetService<ISyntexBuilder>();
            oracleSyntexBuilder.Reset();
            oracleSyntexBuilder.Build().ShouldBe(string.Empty);
        }
    }
}
