using System.Data;
using System.Threading.Tasks;
using Dapper;
using ShadyNagy.DapperInMemory;
using ShadyNagy.DapperManager.Tests.Constants;

namespace ShadyNagy.DapperManager.Tests.Helpers
{
  internal class DatabaseHelper
  {
    public static async Task CreateAllTestTablesAsync()
    {
      await CreateTestInfoTableAsync();
      await CreateTestEmployeeTableAsync();
      
    }

    public static async Task InsertAllRowsTablesAsync()
    {
      await InsertTestEmployeeRowAsync();
      await InsertTestInfoRowAsync();

    }

    public static async Task CreateTestEmployeeTableAsync()
    {
      var connection = new InMemoryConnection(DatabaseConstants.CONNECTION_STRING);
      connection.Open();

      var sql = @"
CREATE TABLE EMPLOYEES
( 
ID NUMBER NOT NULL,
NAME VARCHAR2 NULL
);
";
      var affectedCount = await connection.ExecuteAsync(sql, commandType: CommandType.Text);

      connection.Close();
    }

    public static async Task InsertTestEmployeeRowAsync()
    {
      var connection = new InMemoryConnection(DatabaseConstants.CONNECTION_STRING);
      connection.Open();

      var sql = @"
INSERT INTO EMPLOYEES
(ID, NAME)
VALUES
(1, 'Shady');
";
      var affectedCount = await connection.ExecuteAsync(sql, commandType: CommandType.Text);

      connection.Close();
    }

    public static async Task CreateTestInfoTableAsync()
    {
      var connection = new InMemoryConnection(DatabaseConstants.CONNECTION_STRING);
      connection.Open();

      var sql = @"
CREATE TABLE SYS.ALL_TABLES
( 
OWNER VARCHAR2(30) NOT NULL,
TABLE_NAME VARCHAR2(30) NOT NULL,
TABLESPACE_NAME VARCHAR2(30) NULL,
CLUSTER_NAME VARCHAR2(30) NULL,
IOT_NAME VARCHAR2(30) NULL,
STATUS VARCHAR2(8) NULL
);
";
      var affectedCount = await connection.ExecuteAsync(sql, commandType: CommandType.Text);

      connection.Close();
    }

    public static async Task InsertTestInfoRowAsync()
    {
      var connection = new InMemoryConnection(DatabaseConstants.CONNECTION_STRING);
      connection.Open();

      var sql = @"
INSERT INTO SYS.ALL_TABLES
(OWNER, TABLE_NAME, TABLESPACE_NAME, CLUSTER_NAME, IOT_NAME, STATUS)
VALUES
('SYS', 'DUAL', 'SYSTEM', NULL, NULL, NULL, 'VALID');
";
      var affectedCount = await connection.ExecuteAsync(sql, commandType: CommandType.Text);

      connection.Close();
    }


  }
}
