using Microsoft.Extensions.DependencyInjection;
using ShadyNagy.Dapper.SharedKernel.Interfaces;
using ShadyNagy.DapperManager.Interfaces;
using ShadyNagy.DapperManager.Microsoft;
using ShadyNagy.DapperManager.Oracle;

namespace ShadyNagy.DapperManager.Extensions
{
  public static class ServicesCollectionExtensions
  {
    public static IServiceCollection AddMicrosoftSqlServices(this IServiceCollection services, string connection)
    {
      services.AddScoped<ISqlConnectionFactory>(sp => new MicrosoftConnectionFactory(connection));
      services.AddScoped<ISyntaxBuilder, MicrosoftSyntaxBuilder>();
      services.AddScoped<IDapperService, MicrosoftDapperService>();
      services.AddScoped<IDapperInfoService, MicrosoftDapperInfoService>();

      return services;
    }

    public static IServiceCollection AddMicrosoftSqlServices(this IServiceCollection services, ISqlConnectionFactory sqlConnectionFactory)
    {
      services.AddScoped<ISqlConnectionFactory>(sp => sqlConnectionFactory);
      services.AddScoped<ISyntaxBuilder, MicrosoftSyntaxBuilder>();
      services.AddScoped<IDapperService, MicrosoftDapperService>();
      services.AddScoped<IDapperInfoService, MicrosoftDapperInfoService>();

      return services;
    }

    public static IServiceCollection AddOracleSqlServices(this IServiceCollection services, string connection)
    {
      services.AddScoped<ISqlConnectionFactory>(sp => new OracleConnectionFactory(connection));
      services.AddScoped<ISyntaxBuilder, OracleSyntaxBuilder>();
      services.AddScoped<IDapperService, OracleDapperService>();
      services.AddScoped<IDapperInfoService, OracleDapperInfoService>();

      return services;
    }

    public static IServiceCollection AddOracleSqlServices(this IServiceCollection services, ISqlConnectionFactory sqlConnectionFactory)
    {
      services.AddScoped<ISqlConnectionFactory>(sp => sqlConnectionFactory);
      services.AddScoped<ISyntaxBuilder, OracleSyntaxBuilder>();
      services.AddScoped<IDapperService, OracleDapperService>();
      services.AddScoped<IDapperInfoService, OracleDapperInfoService>();

      return services;
    }
  }
}
