# ShadyNagy.DapperManager

### Dapper ORM  
A NuGet library that will extend your IDbConnection interface
with awesome extensions!  

### Dapper Usage is  
```csharp
string sql = "INSERT INTO Customers (CustomerName) Values (@CustomerName);";

using (var connection =  My.ConnectionFactory())
{
      var affectedRows = connection.Execute(sql, new {CustomerName = "Mark"});
   
      var customers = connection.Query<Customer>("Select * FROM CUSTOMERS).ToList();  
}
```

### Package `ShadyNagy.DapperManager` Usage is  
```csharp
var customers = await oracleDapperService.GetFromAsync<Customer>(tableName);
```
- More information on the tests ShadyNagy.DapperManager.Tests

### Dapper tests  
There are not any tools to test the dapper so this package `ShadyNagy.DapperInMemory` will help you to create tests for Dapper without database
```csharp
var connection = new InMemoryConnection(DatabaseConstants.CONNECTION_STRING);
connection.Open();

var sql = @"SELECT * FROM EMPLOYEES;";
var employees =  (await connection.QueryAsync<Employee>(sql, commandType: CommandType.Text)).ToList();
```
- More information on the tests ShadyNagy.DapperInMemory.Tests
