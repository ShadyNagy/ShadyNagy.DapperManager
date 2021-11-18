using Microsoft.Extensions.DependencyInjection;
using ShadyNagy.Dapper.SharedKernel.Interfaces;
using ShadyNagy.DapperInMemory.Oracle;
using ShadyNagy.DapperManager.Interfaces;
using ShadyNagy.DapperManager.Oracle;
using ShadyNagy.DapperManager.Tests.Constants;

namespace ShadyNagy.DapperManager.Tests.Helpers
{
  internal class DiHelper
  {
    public ServiceProvider ServiceProvider { get; private set; }

    public static DiHelper Create()
    {
      return new DiHelper();
    }

    public DiHelper()
    {
      var services = new ServiceCollection();
      services.AddScoped<ISqlConnectionFactory>(sp => new InMemoryConnectionFactory(DatabaseConstants.CONNECTION_STRING));
      services.AddScoped<ISyntaxBuilder, OracleSyntaxBuilder>();
      services.AddScoped<IDapperService, OracleDapperService>();

      ServiceProvider = services.AddLogging().BuildServiceProvider();
    }

    public T GetService<T>()
    {
      return ServiceProvider.GetService<T>();
    }
  }
}
