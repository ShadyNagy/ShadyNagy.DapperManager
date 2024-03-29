﻿using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using ShadyNagy.DapperInMemory.Tests.Constants;
using Shouldly;
using Xunit;

namespace ShadyNagy.DapperInMemory.Tests
{
  public class CreateTableTest
  {
    [Fact]
    public async Task CreatesTableSuccessAsync()
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
      affectedCount.ShouldBeGreaterThan(0);
    }
  }
}
