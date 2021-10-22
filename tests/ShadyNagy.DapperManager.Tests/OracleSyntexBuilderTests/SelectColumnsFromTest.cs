using ShadyNagy.DapperManager.Interfaces;
using ShadyNagy.DapperManager.Oracle;
using ShadyNagy.DapperManager.Tests.Helpers;
using Shouldly;
using System.Linq;
using Xunit;

namespace ShadyNagy.DapperManager.Tests
{
    public class SelectColumnsFromTest
    {
        private DiHelper _diHelper = DiHelper.Create();

        [Fact]
        public void ReturnsSelectAllSyntexString()
        {
            var tableName = "TABLE";
            var columnsNames = new string[]{ "EMPLOYEES", "COUNTRIES" }.ToList();
            var expected = $"SELECT EMPLOYEES,COUNTRIES FROM {tableName}";


            var oracleSyntexBuilder = _diHelper.GetService<ISyntexBuilder>();
            oracleSyntexBuilder.SelectColumnsFrom(tableName, columnsNames);
            oracleSyntexBuilder.Build().ShouldBe(expected);
        }
    }
}
