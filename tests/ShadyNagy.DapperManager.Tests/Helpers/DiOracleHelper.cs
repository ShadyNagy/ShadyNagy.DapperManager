using Microsoft.Extensions.DependencyInjection;
using ShadyNagy.DapperInMemory.Oracle;
using ShadyNagy.DapperManager.Extensions;
using ShadyNagy.DapperManager.Tests.Constants;

namespace ShadyNagy.DapperManager.Tests.Helpers
{
  internal class DiOracleHelper
  {
    public ServiceProvider ServiceProvider { get; private set; }

    public static DiOracleHelper Create()
    {
      return new DiOracleHelper();
    }

    public DiOracleHelper()
    {
      var services = new ServiceCollection();
      services.AddOracleSqlServices(new InMemoryConnectionFactory(DatabaseConstants.CONNECTION_STRING));

      ServiceProvider = services.AddLogging().BuildServiceProvider();
    }

    public T GetService<T>()
    {
      return ServiceProvider.GetService<T>();
    }
  }
}
