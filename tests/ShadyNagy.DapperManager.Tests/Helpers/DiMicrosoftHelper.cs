using Microsoft.Extensions.DependencyInjection;
using ShadyNagy.DapperInMemory.Oracle;
using ShadyNagy.DapperManager.Extensions;
using ShadyNagy.DapperManager.Tests.Constants;

namespace ShadyNagy.DapperManager.Tests.Helpers
{
  internal class DiMicrosoftHelper
  {
    public ServiceProvider ServiceProvider { get; private set; }

    public static DiMicrosoftHelper Create()
    {
      return new DiMicrosoftHelper();
    }

    public DiMicrosoftHelper()
    {
      var services = new ServiceCollection();
      services.AddMicrosoftSqlServices(new InMemoryConnectionFactory(DatabaseConstants.CONNECTION_STRING));

      ServiceProvider = services.AddLogging().BuildServiceProvider();
    }

    public T GetService<T>()
    {
      return ServiceProvider.GetService<T>();
    }
  }
}
