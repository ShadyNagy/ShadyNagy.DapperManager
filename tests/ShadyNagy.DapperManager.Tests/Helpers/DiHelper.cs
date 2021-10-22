using Microsoft.Extensions.DependencyInjection;
using ShadyNagy.DapperManager.Interfaces;
using ShadyNagy.DapperManager.Oracle;

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
            services.AddScoped<ISqlConnectionFactory, OracleConnectionFactory>();
            services.AddScoped<ISyntexBuilder, OracleSyntexBuilder>();
            services.AddScoped<IDapperService, OracleDapperService>();

            ServiceProvider = services.BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }
    }
}
