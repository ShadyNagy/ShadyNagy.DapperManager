using Dapper;
using ShadyNagy.DapperInMemory.Tests.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

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
    }
}
