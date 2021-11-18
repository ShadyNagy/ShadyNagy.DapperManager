using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ShadyNagy.DapperInMemory.Tests.Constants;

namespace ShadyNagy.DapperInMemory.Tests.Helpers
{
  internal class DatabaseHelper
  {
    public static async Task CreateTestTableAsync()
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

    public static async Task InsertTestRowAsync()
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
  }
}
