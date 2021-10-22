using ShadyNagy.DapperManager.Interfaces;
using ShadyNagy.DapperManager.Oracle;
using ShadyNagy.DapperManager.Tests.Helpers;
using Shouldly;
using Xunit;

namespace ShadyNagy.DapperManager.Tests.OracleSyntexBuilderTests
{
    public class SelectAllFromTest
    {
        private DiHelper _diHelper = DiHelper.Create();

        [Fact]
        public void ReturnsSelectAllSyntexString()
        {
            var tableName = "TABLE";
            var expected = $"SELECT * FROM {tableName}";

            var oracleSyntexBuilder = _diHelper.GetService<ISyntexBuilder>();
            oracleSyntexBuilder.SelectAllFrom(tableName);
            oracleSyntexBuilder.Build().ShouldBe(expected);
        }
    }
}
